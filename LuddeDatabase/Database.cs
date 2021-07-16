/*
 *         lainlib.Database
 * 
 *         lainlib by fybalaban @ 2021
 *         https://www.github.com/fybalaban/lainlib
 */

using lainlib.Cryptography;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace lainlib.Database
{
    /// <summary>
    /// Database for better storing and retreiving data. Does not automatically save data! 
    /// Please use ExportDatabaseImage() method to save object to disk.
    /// </summary>
    public partial class Database
    {
        #region Properties
        /// <summary>
        /// Name of Database. Affects the file name of Database image file.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// GUID of Database. It's there because it's cool to have.
        /// </summary>
        public string ID { get; private set; }

        /// <summary>
        /// Amount of rows in present time.
        /// </summary>
        public int RowCount => Rows.Count;

        /// <summary>
        /// Amount of columns in present time.
        /// </summary>
        public int ColumnCount => ColumnNames.Length;

        /// <summary>
        /// True if any data was changed - needs to be saved to the disk! 
        /// </summary>
        public bool NeedsSaving = false;

        /// <summary>
        /// Shows the status of this Database object.
        /// </summary>
        public DatabaseStatus Status { get; private set; }

        public Cryptography.KeyStore KeyStore { get; private set; }
        public string Path { get; private set; }
        public bool Disposed { get; private set; }
        public string DecryptedFilePath { get; private set; }
        #endregion

        #region Fields
        private string[] ColumnNames = new string[MAXIMUM_COLUMN_COUNT];
        private List<string> Rows = new(MAXIMUM_ROW_COUNT);
        private string Directory;
        private readonly string PathKeystoreFile;
        private readonly string DirectoryKeyStoreFile;
        #endregion

        #region Object Initialization
        /// <summary>
        /// Set up Database from database image file.
        /// </summary>
        /// <param name="pathOfDatabaseFile"></param>
        /// <param name="pathOfKeystoreFile"></param>
        public Database(string pathOfDatabaseFile, string pathOfKeystoreFile)
        {
            if (!pathOfDatabaseFile.Valid())
            {
                throw new ArgumentNullException(nameof(pathOfDatabaseFile));
            }
            if (!pathOfKeystoreFile.Valid())
            {
                throw new ArgumentNullException(nameof(pathOfKeystoreFile));
            }

            Path = pathOfDatabaseFile; // Saves the path to database file
            Directory = IO.ReturnDirectoryFromFullPath(Path); // Saves the directory of database file
            PathKeystoreFile = pathOfKeystoreFile; // Saves the path of .keystore file
            DirectoryKeyStoreFile = IO.ReturnDirectoryFromFullPath(PathKeystoreFile); // Saves the directory of .keystore file

            if (!File.Exists(Path))
            {
                throw new FileNotFoundException("Supplied string does not contain a valid path to database file!", Path);
            }
            if (!File.Exists(PathKeystoreFile))
            {
                throw new FileNotFoundException("Supplied string does not contain a valid path to .keystore file!", PathKeystoreFile);
            }

            if (!ReadKeystore(PathKeystoreFile, out Cryptography.KeyStore keyStore))
            {
                Status = DatabaseStatus.Disposed;
                return;
            }
            KeyStore = keyStore;
            Status = DatabaseStatus.ReadyToConnect;
        }

        /// <summary>
        /// Use this to read old, unencrypted database image files. This is provided only for compability. 
        /// </summary>
        /// <param name="pathOfDatabaseFile"></param>
        /// <param name="store"></param>
        public Database(string pathOfDatabaseFile, KeyStore store)
        {
            if (!pathOfDatabaseFile.Valid())
            {
                throw new ArgumentNullException(nameof(pathOfDatabaseFile));
            }
            if (!File.Exists(pathOfDatabaseFile))
            {
                throw new FileNotFoundException("Supplied string does not contain a valid path to database file!", pathOfDatabaseFile);
            }

            if (IO.ReadBytesFromFile(pathOfDatabaseFile, out byte[] oldFile))
            {
                AESNative native = new(store);
                byte[] cipherBytes = native.EncryptStringToBytes(Encoding.UTF8.GetString(oldFile));
                IO.WriteBytesToFile(cipherBytes, pathOfDatabaseFile);
            }
        }

        /// <summary>
        /// Creates a new Database object, DOES NOT SAVE IT.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="exportDirectory"></param>
        /// <param name="keyStore"></param>
        /// <param name="columnNames"></param>
        public Database(string name, string exportDirectory, Cryptography.KeyStore keyStore, params string[] columnNames)
        {
            if (!name.Valid())
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (!exportDirectory.Valid())
            {
                throw new ArgumentNullException(nameof(exportDirectory));
            }
            if (keyStore is null)
            {
                throw new ArgumentNullException(nameof(keyStore));
            }
            if (columnNames is null)
            {
                throw new ArgumentNullException(nameof(columnNames));
            }
            if (columnNames.Length == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(columnNames));
            }

            Rows = new List<string>(MAXIMUM_ROW_COUNT);

            Name = name;
            ID = Guid.NewGuid().ToString();
            ColumnNames = columnNames;
            NeedsSaving = true;
            Status = DatabaseStatus.Connected;
            Directory = exportDirectory;
            Path = Directory + name + ".db";
            KeyStore = keyStore;

            if (columnNames.Length == 0 || columnNames.Length > MAXIMUM_COLUMN_COUNT)
            {
                throw new ArgumentException("Database object: supplied columnNames array's element count is not within the range of 0 => MAXIMUM_COLUMN_COUNT.");
            }
            if (!Text.Valid(name))
            {
                throw new ArgumentException("Database object: name parameter is not valid.");
            }

            InitializeDatabase();
            NeedsSaving = true;

            CreateTemporary();
        }
        #endregion

        public void Connect()
        {
            if (Status is DatabaseStatus.Connected)
            {
                throw new Exception("Database is already connected.");
            }
            if (Status is DatabaseStatus.Disposed)
            {
                throw new Exception("Database is disposed. Further actions are prohibited.");
            }

            DecryptedFilePath = Directory + Text.GetTempString(@".db");
            File.WriteAllText(DecryptedFilePath, Decrypt(Path, KeyStore)); // Decrypts database image and writes decrypted file to disk.
            ReadTemporary(); // This line populates the Database object.
            Status = DatabaseStatus.Connected;
        }

        /// <summary>
        /// Encrypts temporary DB file, saves to original encrypted DB file and deletes temporary file.
        /// </summary>
        public void Close()
        {
            if (Status is DatabaseStatus.Closing || Status is DatabaseStatus.Disposed)
            {
                return;
            }

            Status = DatabaseStatus.Closing;
            Update();
            Dispose();
            return;
        }

        /// <summary>
        /// Encrypts temporary DB file and saves to original encrypted DB file.
        /// </summary>
        public void Update()
        {
            using (Cryptography.AESNative aes = new(KeyStore))
            {
                aes.Encrypt(DecryptedFilePath, Path);
            }
        }

        private void ReadTemporary()
        {
            if (IO.ReadLinesFromFile(DecryptedFilePath, out IEnumerable<string> list))
            {
                List<string> lines = list.ToList();
                Name = IO.ReturnNameFromFullPath(Path);
                ID = lines[2].Split(':')[1].Trim();
                for (int i = 0; i < IMAGE_FILE_INFO_LINECOUNT; i++) // this loop deletes first lines that contains information about file
                {
                    lines.RemoveAt(0);
                }
                ColumnNames = lines[0].Replace("#", string.Empty).Split(':')[1].Trim().Split(','); // column-name-definition line gets splitted into name tokens
                lines.RemoveAt(0);
                Rows = lines;
            }
        }

        private void CreateTemporary()
        {
            DecryptedFilePath = Directory + Text.GetTempString(@".db");
            WriteTemporary();
        }

        private void WriteTemporary()
        {
            StringBuilder builder = new();
            for (int i = 0; i < ColumnNames.Length; i++)
            {
                builder.Append(string.Format("{0}{1}", ColumnNames[i], ROW_SUBITEMS_DELIMITER));
            }
            string columnNamesLine = builder.ToString().RemoveLastCharacter();

            List<string> lines = new();

            for (int i = 0; i < Rows.Count; i++) // adds the rows line by line to list object.
            {
                lines.Add(Rows[i]);
            }

            IO.WriteLinesToFile(lines, DecryptedFilePath);
            NeedsSaving = true;
        }

        private string Decrypt(string path, Cryptography.KeyStore keystore)
        {
            using (Cryptography.AESNative aes = new(keystore))
            {
                aes.DecryptFile(out string cont, path);
                return cont;
            }
        }

        private void Encrypt(string cont, string loc, Cryptography.KeyStore keystore)
        {
            using (AESNative aes = new(keystore))
            {
                aes.EncryptFile(cont, loc);
            }
        }

        #region Row Methods
        /// <summary>
        /// Adds a new row to Database, returns index of new row.
        /// </summary>
        /// <param name="subitems"></param>
        /// <returns></returns>
        public void AddRow(params string[] subitems)
        {
            if (Status != DatabaseStatus.Connected)
            {
                throw new DatabaseNotConnectedException();
            }

            // Check if supplied subitems do not match up with columns.
            if (subitems.Length != ColumnNames.Length)
            {
                throw new ArgumentException("Database object: Supplied subitems array's element count is not equal to ColumnNames' count.");
            }
            // Check if database is full.
            if (Rows.Count >= MAXIMUM_ROW_COUNT)
            {
                throw new ReachedMaximumRowCountException();
            }
            // Check the lengths of subitems.
            for (int j = 0; j < subitems.Length; j++)
            {
                if (subitems[j].Length > MAXIMUM_SUBITEM_LENGTH)
                {
                    throw new ArgumentOutOfRangeException(nameof(subitems), "Supplied subitem string length exceeds maximum lenght constant.");
                }
            }

            StringBuilder builder = new();
            int i = 0;
            while (i < subitems.Length)
            {
                builder.Append(subitems[i] + ROW_SUBITEMS_DELIMITER);
                i++;
            }
            string fullRow = builder.ToString();
            fullRow = fullRow.Remove(fullRow.Length - 1);
            Rows.Add(fullRow);
            WriteTemporary();
        }

        /// <summary>
        /// Updates contents in row with new subitem array.
        /// </summary>
        /// <param name="index">Index of row to overwrite</param>
        /// <param name="subitems">Subitems to overwrite</param>
        public void UpdateRow(int index, params string[] subitems)
        {
            if (Status != DatabaseStatus.Connected)
            {
                throw new DatabaseNotConnectedException();
            }
            if (index >= 0 && index < Rows.Count && subitems.Length == ColumnCount) // If supplied index is within the amount of Rows and supplied subitems array's element count is equal to column count
            {
                // Check the lengths of subitems.
                for (int j = 0; j < subitems.Length; j++)
                {
                    if (subitems[j].Length > MAXIMUM_SUBITEM_LENGTH)
                    {
                        throw new ArgumentOutOfRangeException(nameof(subitems), "Supplied subitem string length exceeds maximum lenght constant.");
                    }
                }

                StringBuilder builder = new("");
                int i = 0;
                while (i < subitems.Length)
                {
                    builder.AppendFormat("{0}{1}", subitems[i], ROW_SUBITEMS_DELIMITER);
                    i++;
                }
                string newFullRow = Text.RemoveLastCharacter(builder.ToString());
                Rows[index] = newFullRow;
                WriteTemporary();
            }
        }

        /// <summary>
        /// If present, removes row at given index.
        /// </summary>
        /// <param name="index">Index of row</param>
        public void RemoveRowAt(int index)
        {
            if (Status != DatabaseStatus.Connected)
            {
                throw new DatabaseNotConnectedException();
            }

            if (index >= 0 && index < Rows.Count)
            {
                Rows.RemoveAt(index);
                WriteTemporary();
            }
        }
        #endregion

        #region Database Management Tools

        private void Dispose()
        {
            if (!Disposed && (Status is DatabaseStatus.Disposed) == false)
            {
                Name = null;
                Path = null;
                Directory = null;
                ColumnNames = null;
                DecryptedFilePath = null;
                ID = null;
                KeyStore = null;
                Rows = null;
                Disposed = true;
                File.Delete(DecryptedFilePath);
                Status = DatabaseStatus.Disposed;
            }
        }

        /// <summary>
        /// Changes name of Database object. This also changes the name that was used to store the file.
        /// </summary>
        /// <param name="name">Only changed if name is valid</param>
        public void ChangeName(string name)
        {
            if (Status != DatabaseStatus.Connected)
            {
                throw new DatabaseNotConnectedException();
            }
            if (name.Valid())
            {
                Name = name;
                WriteTemporary();
            }
        }

        private void InitializeDatabase()
        {
            StringBuilder stringBuilder = new();
            for (int i = 0; i < ColumnCount; i++)
            {
                stringBuilder.AppendFormat("null{0}", ROW_SUBITEMS_DELIMITER);
            }
            Rows.Add(stringBuilder.ToString().RemoveLastCharacter());
            WriteTemporary();
        }

        /// <summary>
        /// Prints column names and all rows to console.
        /// </summary>
        public void WriteDatabaseToConsole()
        {
            if (Status is DatabaseStatus.Connected)
            {
                StringBuilder builder = new(); // this block creates and prints column names to console.
                string[] sArr = ColumnNames;
                for (int i = 0; i < sArr.Length; i++)
                {
                    string s = sArr[i];
                    builder.Append(s + " | ");
                }
                builder.Remove(builder.ToString().Length - 2, 1);
                Console.WriteLine(builder.ToString());

                for (int i = 0; i < Rows.Count; i++) // this for loop prints all rows to console. 
                {
                    Console.WriteLine(Rows[i]);
                }
            }
            else
            {
                throw new DatabaseNotConnectedException();
            }
        }

        /// <summary>
        /// Returns string object containing all information about Database object.
        /// </summary>
        /// <returns></returns>
        public string GetFormattedDumpOfDatabase()
        {
            if (Status != DatabaseStatus.Connected)
            {
                throw new DatabaseNotConnectedException();
            }

            string dump = string.Empty;
            StringBuilder builder = new();
            builder.AppendLine("_____________________________________________");
            builder.AppendFormat("lainlib.Database // written by fybalaban @ 2021 // version {0}\nCreation time of this dump: {1}\nName of database: {2}\n" +
                "GUID of database: {3}\nRow count: {4}\nColumn count: {5}\n", VERSION, Text.GetDateTimeNow(),
                Name, ID, RowCount, ColumnCount);
            builder.AppendLine("_____________________________________________");
            string formattedColumnNames = string.Empty;
            foreach (string column in ColumnNames)
            {
                formattedColumnNames += " | " + column.ToUpper();
            }
            builder.AppendFormat("Column names: {0}\n", formattedColumnNames);
            builder.AppendLine("_____________________________________________");
            builder.AppendLine("Constants:");
            builder.AppendFormat("> ROW_SUBITEMS_DELIMITER = {0} / {1}", ROW_SUBITEMS_DELIMITER, ROW_SUBITEMS_DELIMITER.ToInteger());
            builder.AppendFormat("> IMAGE_FILE_INFO_LINECOUNT = {0}", IMAGE_FILE_INFO_LINECOUNT);
            builder.AppendFormat("> MAXIMUM_ROW_COUNT = {0}", MAXIMUM_ROW_COUNT);
            builder.AppendFormat("> MAXIMUM_COLUMN_COUNT = {0}", MAXIMUM_COLUMN_COUNT);
            builder.AppendFormat("> MAXIMUM_SUBITEM_LENGTH = {0}", MAXIMUM_SUBITEM_LENGTH);
            builder.AppendLine("_____________________________________________");
            builder.AppendLine("Rows:\n");
            foreach (string row in Rows)
            {
                builder.AppendLine(row);
            }
            builder.AppendLine("_____________________________________________");
            return builder.ToString();
        }
        #endregion

        #region Methods to Search and Retrieve Elements
        /// <summary>
        /// Returns coordinates (row, column) of every matching string. Opposite of GetSubitemAtCoordinates().
        /// </summary>
        /// <param name="lookUp"></param>
        /// <returns></returns>
        public int[] LookUpForString(string lookUp)
        {
            int row = -1, column = -1;
            if (Status != DatabaseStatus.Connected)
            {
                throw new DatabaseNotConnectedException();
            }

            if (lookUp.Valid())
            {
                for (int i = 0; i < Rows.Count; i++) // search all rows
                {
                    List<string> list = Rows[i].Split(ROW_SUBITEMS_DELIMITER).ToList();
                    for (int j = 0; j < list.Count; j++) // search all subitems (columns) of current row.
                    {
                        if (list[j].Contains(lookUp)) // if subitem matches to lookUp string
                        {
                            row = i;
                            column = j;
                        }
                    }
                }
            }
            return new int[2] { row, column };
        }

        /// <summary>
        /// Returns string in given coordinates. Opposite of LookUpForString().
        /// </summary>
        /// <param name="rowIndex">Horizontal position (index) of item</param>
        /// <param name="columnIndex">Vertical position (index) of item</param>
        /// <returns>Returns empty string if arguments are not valid</returns>
        public string GetSubitemAtCoordinates(int rowIndex, int columnIndex)
        {
            return Status != DatabaseStatus.Connected
                ? throw new DatabaseNotConnectedException()
                : rowIndex >= 0 && rowIndex < Rows.Count && columnIndex >= 0 && columnIndex < ColumnNames.Count()
                ? Rows[rowIndex].Split(ROW_SUBITEMS_DELIMITER)[columnIndex]
                : string.Empty;
        }

        /// <summary>
        /// Returns string in given coordinates. Opposite of LookUpForString().
        /// </summary>
        /// <param name="rowIndex">Horizontal position (index) of item</param>
        /// <param name="columnName">Name of column</param>
        /// <returns>Returns empty string if arguments are not valid</returns>
        public string GetSubitemAtCoordinates(int rowIndex, string columnName)
        {
            return Status != DatabaseStatus.Connected
                ? throw new DatabaseNotConnectedException()
                : rowIndex >= 0 && rowIndex < RowCount && ColumnNames.Contains(columnName)
                ? Rows[rowIndex].Split(ROW_SUBITEMS_DELIMITER)[Collections.GetIndexOfElement(ColumnNames, columnName)]
                : string.Empty;
        }

        /// <summary>
        /// Returns subitems in given column index. (Up to down / vertically) Returns null if index is not valid.
        /// </summary>
        /// <param name="columnIndex">Index of column</param>
        /// <returns></returns>
        public string[] ReturnAllSubitemsInColumn(int columnIndex)
        {
            if (Status != DatabaseStatus.Connected)
            {
                throw new DatabaseNotConnectedException();
            }
            if (columnIndex >= 0 && columnIndex < ColumnNames.Length)
            {
                List<string> list = new();
                for (int i = 0; i < Rows.Count; i++)
                {
                    list.Add(Rows[i].Split(ROW_SUBITEMS_DELIMITER)[columnIndex]);
                }
                return list.ToArray();
            }
            return null;
        }

        /// <summary>
        /// Returns subitems in given column index. (Up to down / vertically) Returns null if index is not valid.
        /// </summary>
        /// <param name="columnName">Name of column</param>
        /// <returns></returns>
        public string[] ReturnAllSubitemsInColumn(string columnName)
        {
            if (Status != DatabaseStatus.Connected)
            {
                throw new DatabaseNotConnectedException();
            }
            if (ColumnNames.Contains(columnName) == true)
            {
                int indexOfColumnName = 0;
                for (int i = 0; i < ColumnNames.Length; i++)
                {
                    if (columnName == ColumnNames[i])
                    {
                        indexOfColumnName = i; // finds index of columnName in ColumnNames
                    }
                }

                return ReturnAllSubitemsInColumn(indexOfColumnName);
            }
            return null;
        }

        /// <summary>
        /// Returns all subitems in indexed row.
        /// </summary>
        /// <param name="rowIndex">Index of row</param>
        /// <returns>If index is not valid returns null</returns>
        public string[] ReturnSubitemsInRow(int rowIndex)
        {
            return Status != DatabaseStatus.Connected
                ? throw new DatabaseNotConnectedException()
                : rowIndex >= 0 && rowIndex < Rows.Count ? Rows[rowIndex].Split(ROW_SUBITEMS_DELIMITER) : null;
        }
        #endregion

        #region Miscellaneous Methods
        /// <summary>
        /// Opens and reads KeyStore file.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="keystore"></param>
        /// <returns></returns>
        private bool ReadKeystore(string path, out Cryptography.KeyStore keystore)
        {
            keystore = null;
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"File '{path}' is not found.", path);
            }
            keystore = Cryptography.AESResourceSupplier.ReadKeyStore(path);
            return true;
        }
        #endregion

        #region Exceptions
        /// <summary>
        /// Occurs when maximum amount of rows are reached. 
        /// </summary>
        public class ReachedMaximumRowCountException : Exception
        {
            /// <summary>
            /// No arguments, no details. Only for giving information.
            /// </summary>
            public ReachedMaximumRowCountException()
            {
            }

            /// <summary>
            /// Returns "Reached maximum amount of rows."
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return @"Reached maximum amount of rows.";
            }
        }

        public class DatabaseNotConnectedException : Exception
        {
            public DatabaseNotConnectedException() { }
            public DatabaseNotConnectedException(string message) : base(message) { }

            public override string ToString()
            {
                return @"Connection to a database is not present.";
            }
        }
        #endregion

        #region Constants
        /// <summary>
        /// Constant for splitting subitems of rows. Character ',' and 44 in ASCII.
        /// </summary>
        public const char ROW_SUBITEMS_DELIMITER = ',';
        /// <summary>
        /// Database image file has 6 lines to contain information about Database. Good to know.
        /// </summary>
        public const int IMAGE_FILE_INFO_LINECOUNT = 6;
        /// <summary>
        /// Maximum allowed row count.
        /// </summary>
        public const int MAXIMUM_ROW_COUNT = 8192;
        /// <summary>
        /// Maximum allowed column count.
        /// </summary>
        public const int MAXIMUM_COLUMN_COUNT = 32;
        /// <summary>
        /// Maximum length of any subitem string.
        /// </summary>
        public const int MAXIMUM_SUBITEM_LENGTH = 1024;
        /// <summary>
        /// Version of this source code.
        /// </summary>
        public const int VERSION = 5;
        #endregion
    }
}