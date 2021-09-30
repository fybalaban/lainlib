/*
 *         lainlib.Cryptography
 * 
 *         lainlib by fybalaban @ 2021
 *         https://www.github.com/fybalaban/lainlib
*/

using System;
using System.IO;
using System.Security.Cryptography;

namespace lainlib.Cryptography
{
    /// <summary>
    /// Provides methods for file, string and byte array encryption/decryption with AES. Uses System.Security.Cryptography.AesCryptoServiceProvider. Native code is faster with big loads.
    /// </summary>
    public class AESNative : IDisposable
    {
        #region Properties
        /// <summary>
        /// Specifies the type of padding to apply when the message data block is shorter than the full number of bytes needed for a cryptographic operation
        /// </summary>
        public PaddingMode PaddingMode { get; private set; }

        /// <summary>
        /// Specifies the block cipher mode to use for encryption
        /// </summary>
        public CipherMode CipherMode { get; private set; }

        /// <summary>
        /// The key for the symmetric algorithm
        /// </summary>
        public byte[] Key { get; private set; }

        /// <summary>
        /// The size, in bits, of the key used by the symmetric algorithm
        /// </summary>
        public int KeySize { get; private set; }

        /// <summary>
        /// The initialization vector to use for the symmetric algorithm
        /// </summary>
        public byte[] IV { get; private set; }
        #endregion

        #region Object Creation
        /// <summary>
        /// Initializes AESNative with default parameters.
        /// </summary>
        public AESNative()
        {
            PaddingMode = PaddingMode.PKCS7;
            CipherMode = CipherMode.CBC;
            KeySize = 256;
            Key = AESResourceSupplier.GetKey(256);
            IV = AESResourceSupplier.GetIV();
        }

        /// <summary>
        /// Initializes AESNative with a Key.
        /// </summary>
        /// <param name="keySize">Size of key, must a be valid AES key size</param>
        /// <param name="key">The key</param>
        public AESNative(int keySize, byte[] key)
        {
            if (!AESResourceSupplier.IsValidKeySize(keySize))
                throw new ArgumentException("Argument: 'keySize' is not a valid size for AES algorithm.");
            if (key is null || key.Length is 0)
                throw new ArgumentNullException(nameof(key));

            PaddingMode = PaddingMode.PKCS7;
            CipherMode = CipherMode.CBC;
            KeySize = keySize;
            Key = key;
            IV = AESResourceSupplier.GetIV();
        }

        /// <summary>
        /// Initializes AESNative with a Key and an Initialization Vector.
        /// </summary>
        /// <param name="keySize">Size of key, must a be valid AES key size</param>
        /// <param name="key">The key</param>
        /// <param name="iv">The Initialization Vector</param>
        public AESNative(int keySize, byte[] key, byte[] iv)
        {
            if (!AESResourceSupplier.IsValidKeySize(keySize))
                throw new ArgumentException("Argument: 'keySize' is not a valid size for AES algorithm.");
            if (key is null || key.Length is 0)
                throw new ArgumentNullException(nameof(key));
            if (iv is null || iv.Length is 0)
                throw new ArgumentNullException(nameof(iv));

            PaddingMode = PaddingMode.PKCS7;
            CipherMode = CipherMode.CBC;
            KeySize = keySize;
            Key = key;
            IV = iv;
        }

        /// <summary>
        /// Initializes AESNative using a KeyStore object.
        /// </summary>
        /// <param name="keystore"></param>
        public AESNative(KeyStore keystore)
        {
            if (!AESResourceSupplier.IsValidKeySize(keystore.KeySize))
                throw new ArgumentException("Argument: 'keySize' is not a valid size for AES algorithm.");
            if (keystore.Key is null || keystore.Key.Length is 0 || keystore.IV is null || keystore.IV.Length is 0)
                throw new ArgumentNullException(nameof(keystore));

            PaddingMode = PaddingMode.PKCS7;
            CipherMode = CipherMode.CBC;
            KeySize = keystore.KeySize;
            Key = keystore.Key;
            IV = keystore.IV;
        }

        /// <summary>
        /// Initializes AESNative with a Key, an Initialization Vector and a padding mode.
        /// </summary>
        /// <param name="keySize">Size of key, must a be valid AES key size</param>
        /// <param name="key">The key</param>
        /// <param name="iv">The Initialization Vector</param>
        /// <param name="paddingMode">The padding mode</param>
        public AESNative(int keySize, byte[] key, byte[] iv, PaddingMode paddingMode)
        {
            if (!AESResourceSupplier.IsValidKeySize(keySize))
                throw new ArgumentException("Argument: 'keySize' is not a valid size for AES algorithm.");
            if (key is null || key.Length is 0)
                throw new ArgumentNullException(nameof(key));
            if (iv is null || iv.Length is 0)
                throw new ArgumentNullException(nameof(iv));

            PaddingMode = paddingMode;
            CipherMode = CipherMode.CBC;
            KeySize = keySize;
            Key = key;
            IV = iv;
        }

        /// <summary>
        /// Initializes AESNative with a Key, an Initialization Vector and a cipher mode.
        /// </summary>
        /// <param name="keySize">Size of key, must a be valid AES key size</param>
        /// <param name="key">The key</param>
        /// <param name="iv">The Initialization Vector</param>
        /// <param name="cipherMode">The cipher mode</param>
        public AESNative(int keySize, byte[] key, byte[] iv, CipherMode cipherMode)
        {
            if (!AESResourceSupplier.IsValidKeySize(keySize))
                throw new ArgumentException("Argument: 'keySize' is not a valid size for AES algorithm.");
            if (key is null || key.Length is 0)
                throw new ArgumentNullException(nameof(key));
            if (iv is null || iv.Length is 0)
                throw new ArgumentNullException(nameof(iv));

            PaddingMode = PaddingMode.PKCS7;
            CipherMode = cipherMode;
            KeySize = keySize;
            Key = key;
            IV = iv;
        }

        /// <summary>
        /// Initializes AESNative with a Key, an Initialization Vector, a padding mode and cipher mode.
        /// </summary>
        /// <param name="keySize">Size of key, must a be valid AES key size</param>
        /// <param name="key">The key</param>
        /// <param name="iv">The Initialization Vector</param>
        /// <param name="paddingMode">The padding mode</param>
        /// <param name="cipherMode">The cipher mode</param>
        public AESNative(int keySize, byte[] key, byte[] iv, PaddingMode paddingMode, CipherMode cipherMode)
        {
            if (!AESResourceSupplier.IsValidKeySize(keySize))
                throw new ArgumentException("Argument: 'keySize' is not a valid size for AES algorithm.");
            if (key is null || key.Length is 0)
                throw new ArgumentNullException(nameof(key));
            if (iv is null || iv.Length is 0)
                throw new ArgumentNullException(nameof(iv));

            PaddingMode = paddingMode;
            CipherMode = cipherMode;
            KeySize = keySize;
            Key = key;
            IV = iv;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Encrypts supplied string and returns encrypted output in byte array.
        /// </summary>
        /// <param name="plainText">Text to encrypt</param>
        /// <returns></returns>
        public byte[] EncryptStringToBytes(string plainText)
        {
            if (plainText is null or @"")
                throw new ArgumentNullException(nameof(plainText));

            using (AesCryptoServiceProvider aes = new())
            {
                aes.Padding = PaddingMode;
                aes.Mode = CipherMode;
                aes.KeySize = KeySize;
                aes.Key = Key;
                aes.IV = IV;

                ICryptoTransform encryptor = aes.CreateEncryptor(); // Create an encryptor to perform the stream transform
                using (MemoryStream ms = new()) // Create the stream used to retrieve encrypted data
                {
                    using (CryptoStream cs = new(ms, encryptor, CryptoStreamMode.Write)) // Create the stream to perform the actual encryption
                    {
                        using (StreamWriter sw = new(cs)) // Create StreamWriter to access and write data to CryptoStream
                            sw.Write(plainText); // Write data to the stream
                        return ms.ToArray(); // Return encrypted data from MemoryStream
                    }
                }
            }
        }

        /// <summary>
        /// Decrypts cipher byte array to plain text.
        /// </summary>
        /// <param name="cipherText">Byte[] to decrypt</param>
        /// <returns></returns>
        public string DecryptBytesToString(byte[] cipher)
        {
            if (cipher is null || cipher.Length is 0)
                throw new ArgumentNullException(nameof(cipher));

            using (AesCryptoServiceProvider aes = new())
            {
                aes.Padding = PaddingMode;
                aes.Mode = CipherMode;
                aes.KeySize = KeySize;
                aes.Key = Key;
                aes.IV = IV;

                ICryptoTransform decryptor = aes.CreateDecryptor(); // Create a decryptor to perform the stream transform.
                using (MemoryStream ms = new(cipher)) // Create the streams used for decryption.
                {
                    using (CryptoStream cs = new(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new(cs))
                            return sr.ReadToEnd(); // Read the decrypted bytes from the decrypting stream and place them in a string.
                    }
                }
            }
        }

        /// <summary>
        /// Encrypts supplied byte array using encryptor from initialized AES crypto supplier
        /// </summary>
        /// <param name="original">Original array to encrypt</param>
        /// <returns>If successful, returns encrypted byte array otherwise returns null</returns>
        public byte[] EncryptBytes(byte[] original)
        {
            if (original is null || original.Length is 0)
                throw new ArgumentNullException(nameof(original));

            using (AesCryptoServiceProvider aes = new())
            {
                aes.Padding = PaddingMode;
                aes.Mode = CipherMode;
                aes.KeySize = KeySize;
                aes.Key = Key;
                aes.IV = IV;

                using (ICryptoTransform encryptor = aes.CreateEncryptor())
                    return PerformCryptography(original, encryptor);
            }
        }

        /// <summary>
        /// Decrypts supplied byte array using decryptor from initialized AES crypto supplier
        /// </summary>
        /// <param name="cipher">Ciphered data array to decrypt</param>
        /// <returns></returns>
        public byte[] DecryptBytes(byte[] cipher)
        {
            if (cipher is null || cipher.Length is 0)
                throw new ArgumentNullException(nameof(cipher));

            using (AesCryptoServiceProvider aes = new())
            {
                aes.Padding = PaddingMode;
                aes.Mode = CipherMode;
                aes.KeySize = KeySize;
                aes.Key = Key;
                aes.IV = IV;

                using (ICryptoTransform decryptor = aes.CreateDecryptor())
                    return PerformCryptography(cipher, decryptor);
            }
        }

        /// <summary>
        /// Used to encrypt/decrypt bytes of data. 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="transform"></param>
        /// <returns></returns>
        private static byte[] PerformCryptography(byte[] data, ICryptoTransform transform)
        {
            using (MemoryStream ms = new())
            {
                using (CryptoStream cs = new(ms, transform, CryptoStreamMode.Write))
                {
                    cs.Write(data, 0, data.Length);
                    cs.FlushFinalBlock();
                    return ms.ToArray();
                }
            }
        }

        /// <summary>
        /// Reads file to a byte array, performs encryption on read byte array, saves cipher bytes to same filename with '.enc' appended
        /// </summary>
        /// <param name="filePath">Path to file to encrypt</param>
        /// <returns>Returns the path to encrypted file</returns>
        public string EncryptFile(string filePath)
        {
            if (filePath is null or @"")
                throw new ArgumentNullException(nameof(filePath));
            if (!File.Exists(filePath))
                throw new FileNotFoundException("File not found!", filePath);

            byte[] originalFileContent = File.ReadAllBytes(filePath);
            byte[] encryptedContent = EncryptBytes(originalFileContent);
            File.WriteAllBytes(filePath + ".enc", encryptedContent);
            return filePath + ".enc";
        }

        /// <summary>
        /// Reads encrypted file to a byte array, performs decryption on read byte array, saves decrypted content to same filename, removing '.enc'
        /// </summary>
        /// <param name="filePath">Path to encrypted file</param>
        /// <returns>Returns the path to decrypted file</returns>
        public string DecryptFile(string filePath)
        {
            if (filePath is null or @"")
                throw new ArgumentNullException(nameof(filePath));
            if (!File.Exists(filePath))
                throw new FileNotFoundException("File not found!", filePath);

            byte[] encryptedContent = File.ReadAllBytes(filePath);
            byte[] decryptedContent = DecryptBytes(encryptedContent);
            File.WriteAllBytes(filePath.Replace(".enc", string.Empty), decryptedContent);
            return filePath.Replace(".enc", string.Empty);
        }
        #endregion

        #region IDisposable Implementation
        private bool disposedValue = false; // To detect redundant calls

        /// <summary>
        /// Dispose and free any resource used by this class.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Key = null;
                    IV = null;
                }

                disposedValue = true;
            }
        }

        /// <summary>
        /// Dispose and free any resource used by this class.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}