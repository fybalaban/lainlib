/*
 *         lainlib.ConsoleMethods
 * 
 *         lainlib by fybalaban @ 2021
 *         https://www.github.com/fybalaban/lainlib
 */

#pragma warning disable IDE1006 // This line disables the Naming Rule Violation warning. (Visual Studio 2017)
using System;
using System.Drawing;

namespace lainlib
{
    /// <summary>
    /// C++ like methods for faster typing and cooler looking codes. Include with 'using static lainlib.ConsoleMethods'.
    /// </summary>
    public static class ConsoleMethods
    {
        /// <summary>
        /// "Press any key to continue..."
        /// </summary>
        public static void pause()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }

        /// <summary>
        /// return Console.Read();
        /// </summary>
        /// <returns></returns>
        public static int read()
        {
            return Console.Read();
        }

        /// <summary>
        /// return Console.ReadLine();
        /// </summary>
        /// <returns></returns>
        public static string readln()
        {
            return Console.ReadLine();
        }

        /// <summary>
        /// Changes console title if new_title is a valid string.
        /// </summary>
        /// <param name="new_title"></param>
        public static void tit(string new_title)
        {
            if (Text.Valid(new_title))
            {
                Console.Title = new_title;
            }
        }

        #region write
        /// <summary>
        /// Console.Write(buffer.ToString());
        /// </summary>
        /// <param name="buffer"></param>
        public static void write(object buffer)
        {
            Console.Write(buffer.ToString());
        }

        /// <summary>
        /// Console.Write(buffer);
        /// </summary>
        /// <param name="buffer"></param>
        public static void write(string buffer)
        {
            Console.Write(buffer);
        }

        /// <summary>
        /// Console.Write(format, args);
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void write(string format, params object[] args)
        {
            Console.Write(format, args);
        }

        /// <summary>
        /// Console.Write(buffer.ToString(), color);
        /// </summary>
        /// <param name="buffer"></param>
        public static void write(object buffer, ConsoleColor color)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(buffer.ToString());
            Console.ForegroundColor = defaultColor;
        }

        /// <summary>
        /// Console.Write(buffer, color);
        /// </summary>
        /// <param name="buffer"></param>
        public static void write(string buffer, ConsoleColor color)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(buffer);
            Console.ForegroundColor = defaultColor;
        }

        /// <summary>
        /// Console.Write(format, color, args);
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void write(string format, ConsoleColor color, params object[] args)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(format, args);
            Console.ForegroundColor = defaultColor;
        }
        #endregion

        #region writeln
        /// <summary>
        /// Console.WriteLine(buffer.ToString());
        /// </summary>
        /// <param name="buffer"></param>
        public static void writeln(object buffer)
        {
            Console.WriteLine(buffer.ToString());
        }

        /// <summary>
        /// Console.WriteLine(buffer);
        /// </summary>
        /// <param name="buffer"></param>
        public static void writeln(string buffer)
        {
            Console.WriteLine(buffer);
        }

        /// <summary>
        /// Console.WriteLine(format, args);
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void writeln(string format, params object[] args)
        {
            Console.WriteLine(format, args);
        }

        /// <summary>
        /// Console.WriteLine(buffer.ToString(), color);
        /// </summary>
        /// <param name="buffer"></param>
        public static void writeln(object buffer, ConsoleColor color)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(buffer.ToString());
            Console.ForegroundColor = defaultColor;
        }

        /// <summary>
        /// Console.WriteLine(buffer, color);
        /// </summary>
        /// <param name="buffer"></param>
        public static void writeln(string buffer, ConsoleColor color)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(buffer);
            Console.ForegroundColor = defaultColor;
        }

        /// <summary>
        /// Console.WriteLine(format, color, args);
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void writeln(string format, ConsoleColor color, params object[] args)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(format, args);
            Console.ForegroundColor = defaultColor;
        }
        #endregion

        /// <summary>
        /// Overwrites last line with new content.
        /// </summary>
        /// <param name="buffer"></param>
        public static void updateln(string buffer)
        {
            Console.Write(string.Format(@"\r{0}                                ", buffer));
        }

        /// <summary>
        /// Ask the user a question, if the user enters "Y" or "y", this method returns <see langword="true"/> otherwise false.
        /// </summary>
        /// <param name="question">Please use a format that is understandable by the user.</param>
        /// <returns></returns>
        public static bool ask(string question = @"Question? (Y/N): ")
        {
            bool answered = false;
            while (!answered)
            {
                write(question);
                string input = readln();

                if (input == "Y" || input == "y")
                {
                    return true;
                }
                else if (input == "N" || input == "n")
                {
                    return false;
                }
                answered = false;
            }
            return false;
        }

        /// <summary>
        /// Handles the coloring task of Console. Just give the color argument, this method will extract color components and apply colors to Console accordingly.
        /// Example argument: fc, 39, 2f, 3E, f9
        /// </summary>
        /// <param name="colorcode"></param>
        /// <returns></returns>
        public static bool color(string colorcode)
        {
            colorcode = colorcode.ToUpper();
            if (!colorcode.Valid())
            {
                return false;
            }
            if (colorcode.Length != 2)
            {
                return false;
            }
            if (colorcode[0] == colorcode[1])
            {
                return false;
            }
            if (!colorcode.ContainsCharacters(new char[16] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' }))
            {
                return false;
            }

            switch (colorcode[0])
            {
                case '0':
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
                case '1':
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    break;
                case '2':
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    break;
                case '3':
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    break;
                case '4':
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    break;
                case '5':
                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    break;
                case '6':
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    break;
                case '7':
                    Console.BackgroundColor = ConsoleColor.White;
                    break;
                case '8':
                    Console.BackgroundColor = ConsoleColor.Gray;
                    break;
                case '9':
                    Console.BackgroundColor = ConsoleColor.Blue;
                    break;
                case 'A':
                    Console.BackgroundColor = ConsoleColor.Green;
                    break;
                case 'B':
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    break;
                case 'C':
                    Console.BackgroundColor = ConsoleColor.Red;
                    break;
                case 'D':
                    Console.BackgroundColor = ConsoleColor.Magenta;
                    break;
                case 'E':
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    break;
                case 'F':
                    Console.BackgroundColor = ConsoleColor.White;
                    break;
            }
            switch (colorcode[1])
            {
                case '0':
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
                case '1':
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    break;
                case '2':
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    break;
                case '3':
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    break;
                case '4':
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case '5':
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    break;
                case '6':
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case '7':
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case '8':
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case '9':
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case 'A':
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case 'B':
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case 'C':
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case 'D':
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case 'E':
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case 'F':
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
            return true;
        }

    }
}
#pragma warning restore IDE1006