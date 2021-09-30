/*
 *         lainlib
 * 
 *         lainlib by fybalaban @ 2021
 *         https://www.github.com/fybalaban/lainlib
 */

#pragma warning disable IDE1006 // This line disables the Naming Rule Violation warning
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text.RegularExpressions;
using System.Threading;

namespace lainlib
{
    /// <summary>
    /// This class contains C++ like named methods to make using System.Console more convinient. Include with 'using static lainlib.ConsoleMethods'
    /// </summary>
    public static class ConsoleMethods
    {
        /// <summary>
        /// Selects words wrapped in square brackets. [Hey] is a match, and [Hello world] is a match, but [hey there[ is not.
        /// </summary>
        public static readonly Regex SquareBracketsTagMatcher = new(@"(\[[^\[]*\])", RegexOptions.Compiled);

        /// <summary>
        /// Selects words wrapped in curly brackets. {Hey} is a match, and {Hello world} is a match, but {hey there{ is not.
        /// </summary>
        public static readonly Regex CurlyBracketsTagMatcher = new(@"(\{[^\{]*\})", RegexOptions.Compiled);

        #region println
        /// <summary>
        /// Prints text representation of object, followed by the line terminator
        /// </summary>
        /// <param name="value">An object to print</param>
        public static void println(object value) => Console.WriteLine(value);

        /// <summary>
        /// Prints text representation of object using specified format, followed by the line terminator
        /// </summary>
        /// <param name="format">A composite format string</param>
        /// <param name="arg">An object to print</param>
        public static void println(string format, object arg) => Console.WriteLine(format, arg);

        /// <summary>
        /// Prints text representation of specified array of objects using specified format, followed by the line terminator
        /// </summary>
        /// <param name="format">A composite format string</param>
        /// <param name="arg">An array of objects to print</param>
        public static void println(string format, params object[] arg) => Console.WriteLine(format, arg);

        /// <summary>
        /// Prints text representation of specified object to console window with specified forecolor, followed by the line terminator
        /// </summary>
        /// <param name="value">An object to print</param>
        /// <param name="fColor">A ConsoleColor value to use</param>
        public static void println(object value, ConsoleColor fColor)
        {
            ConsoleColor _fg = Console.ForegroundColor; // save consolecolor state
            Console.ForegroundColor = fColor; // apply new color
            println(value); // write string
            Console.ForegroundColor = _fg; // set old consolecolor state
        }

        /// <summary>
        /// Prints text representation of specified object to console window with specified forecolor and backcolor, followed by the line terminator
        /// </summary>
        /// <param name="value">An object to print</param>
        /// <param name="bColor">A ConsoleColor value to use</param>
        /// <param name="fColor">A ConsoleColor value to use</param>
        public static void println(object value, ConsoleColor bColor, ConsoleColor fColor)
        {
            ConsoleColor _bg = Console.BackgroundColor;
            ConsoleColor _fg = Console.ForegroundColor; // save consolecolor state

            Console.BackgroundColor = bColor;
            Console.ForegroundColor = fColor; // apply new color
            println(value); // write string

            Console.BackgroundColor = _bg;
            Console.ForegroundColor = _fg; // set old consolecolor state
        }

        /// <summary>
        /// Prints words wrapped with specified bracket characters in specified forecolors, followed by the line terminator. Check /doc/ConsoleMethods.md for usage examples
        /// </summary>
        /// <param name="format">An formatted string to print</param>
        /// <param name="cBracket">First bracket used in words, pass '{' or '['</param>
        /// <param name="fColor">A ConsoleColor value to use</param>
        public static void println(string format, char cBracket, ConsoleColor fColor) => print(format + '\n', cBracket, fColor);

        /// <summary>
        /// Prints words wrapped with specified bracket characters in specified forecolor, followed by the line terminator. Check /doc/ConsoleMethods.md for usage examples
        /// </summary>
        /// <param name="format">An formatted string to print</param>
        /// <param name="cBracket">First bracket used in words, pass '{' or '['</param>
        /// <param name="aColors">An array of ConsoleColor values to use, count must be equal to wrapped words in format string</param>
        public static void println(string format, char cBracket, params ConsoleColor[] aColors) => print(format + '\n', cBracket, aColors);
        #endregion

        #region print

        /// <summary>
        /// Prints text representation of specified value
        /// </summary>
        /// <param name="value">A value to write</param>
        public static void print(object value) => Console.Write(value);

        /// <summary>
        /// Prints elements of array in specified amount of elements per line
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">Array containing elements to print of</param>
        /// <param name="itemPerLine"></param>
        public static void print<T>(T[] array, int itemPerLine)
        {
            if (array is not null && array.Length is not 0 && itemPerLine is not 0)
            {
                int count = 0;
                for (int i = 0; i < array.Length; i++)
                {
                    if (count < itemPerLine)
                    {
                        print($"{array[i]} ");
                        count++;
                    }
                    else if (count == itemPerLine)
                    {
                        count = 0;
                        i -= 1;
                        print($"\n");
                    }
                }
                print("\n");
            }
        }

        /// <summary>
        /// Prints text representation of specified object using specified format
        /// </summary>
        /// <param name="format">A composite format string</param>
        /// <param name="arg">An object to write</param>
        public static void print(string format, object arg) => Console.Write(format, arg);

        /// <summary>
        /// Prints text representation of specified array of objects using specified format
        /// </summary>
        /// <param name="format">A composite format string</param>
        /// <param name="arg">An array of objects to write</param>
        public static void print(string format, params object[] arg) => Console.Write(format, arg);

        /// <summary>
        /// Prints text representation of specified object to console window with specified forecolor
        /// </summary>
        /// <param name="value">An object to write</param>
        /// <param name="fColor">A ConsoleColor value to use</param>
        public static void print(object value, ConsoleColor fColor)
        {
            ConsoleColor _fg = Console.ForegroundColor; // save consolecolor state
            Console.ForegroundColor = fColor; // apply new color
            print(value); // write string
            Console.ForegroundColor = _fg; // set old consolecolor state
        }

        /// <summary>
        /// Prints text representation of specified object to console window with specified forecolor and backcolor
        /// </summary>
        /// <param name="value">An object to write</param>
        /// <param name="bColor">A ConsoleColor value to use</param>
        /// <param name="fColor">A ConsoleColor value to use</param>
        public static void print(object value, ConsoleColor bColor, ConsoleColor fColor)
        {
            ConsoleColor _bg = Console.BackgroundColor;
            ConsoleColor _fg = Console.ForegroundColor; // save consolecolor state

            Console.BackgroundColor = bColor;
            Console.ForegroundColor = fColor; // apply new color
            print(value); // write string

            Console.BackgroundColor = _bg;
            Console.ForegroundColor = _fg; // set old consolecolor state
        }

        /// <summary>
        /// Prints words wrapped with specified bracket characters in specified forecolor. Check /doc/ConsoleMethods.md for usage examples
        /// </summary>
        /// <param name="format">An formatted string to print</param>
        /// <param name="cBracket">First bracket used in words, pass '{' or '['</param>
        /// <param name="fColor">A ConsoleColor value to use</param>
        public static void print(string format, char cBracket, ConsoleColor fColor) => print(format, cBracket, new ConsoleColor[] { fColor });

        /// <summary>
        /// Prints words wrapped with specified bracket characters in specified forecolors. Check /doc/ConsoleMethods.md for usage examples
        /// </summary>
        /// <param name="format">An formatted string to print</param>
        /// <param name="cBracket">First bracket used in words, pass '{' or '['</param>
        /// <param name="aColors">An array of ConsoleColor values to use, count must be equal to wrapped words in format string</param>
        public static void print(string format, char cBracket, params ConsoleColor[] aColors)
        {
            ConsoleColor _bg = Console.BackgroundColor;
            ConsoleColor _fg = Console.ForegroundColor;

            int argindex = 0;
            List<string> pieces = (cBracket is '{' or '}') ? CurlyBracketsTagMatcher.Split(format).ToList() : SquareBracketsTagMatcher.Split(format).ToList();
            pieces.RemoveAll(x => x is @"" || x == string.Empty);
            for (int i = 0; i < pieces.Count; i++)
            {
                string piece = pieces[i];
                if (piece.StartsWith(cBracket.ToString()) && piece.EndsWith(cBracket is '{' ? "}" : "]"))
                {
                    Console.ForegroundColor = aColors[argindex];
                    piece = piece[1..^1];
                    argindex = aColors.Length is 1 ? argindex : argindex + 1;
                }
                Console.Write(piece);
                Console.BackgroundColor = _bg;
                Console.ForegroundColor = _fg;
            }
        }
        #endregion

#nullable enable
        /// <summary>
        /// Reads next line of characters from console
        /// </summary>
        /// <returns></returns>
        public static string? readln() => Console.ReadLine();

        /// <summary>
        /// Reads next character from console
        /// </summary>
        /// <returns>The next character if there's any, or -1 if there is nothing to read</returns>
        public static int read()
        {
            return Console.Read();
        }

        /// <summary>
        /// Prints supplied question in "<question> (y/n): " format and waits for appropriate answer. Returns true if user enters Y/y and false if user enters N/n.
        /// </summary>
        /// <param name="question">Please supply the question without (y/n) or semicolon, those will be added by this method</param>
        /// <returns></returns>
        public static bool ask(string question)
        {
            while (true)
            {
                Console.Write(string.Format("{0} (Y/n): ", question));
                string? answer = Console.ReadLine()?.ToLower();

                if (answer is null)
                    println("please enter with (Y)es or (N)o");
                else if (answer is "y")
                    return true;
                else if (answer is "n")
                    return false;
                else
                    println("please enter with (Y)es or (N)o");
            }
        }
#nullable restore

        /// <summary>
        /// Reads next line of characters, but writes a specified character instead of showing what was written. Useful for getting passwords from user in a secure way
        /// </summary>
        /// <param name="passwordChar">Character that will be printed instead of user input, '*' by default</param>
        /// <returns>Returns read line of characters in SecureString object</returns>
        public static SecureString readhidden(char passwordChar = '*')
        {
            SecureString str = new();
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                    break;
                if (key.Key == ConsoleKey.Backspace && str.Length > 0)
                {
                    str.RemoveAt(str.Length - 1);
                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                    Console.Write(" ");
                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                }
                else if (key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.DownArrow || key.Key == ConsoleKey.LeftArrow || key.Key == ConsoleKey.RightArrow)
                {
                    // do absolutely nothing here, user shall not get out of this dialog
                }
                else if (key.Key != ConsoleKey.Backspace)
                {
                    str.AppendChar(key.KeyChar);
                    Console.Write(passwordChar);
                }
            }
            return str;
        }

        /// <summary>
        /// Prints "Press any key to continue..." and waits for any key to be pressed
        /// </summary>
        public static void pause()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// Shows a spin animation for specified amout of time
        /// </summary>
        /// <param name="holdMilliseconds">Amount of time to execute the animation in milliseconds</param>
        public static void spin(double holdMilliseconds)
        {
            DateTime start = DateTime.UtcNow;
            int counter = 0;
            while (DateTime.UtcNow - start < TimeSpan.FromMilliseconds(holdMilliseconds))
            {
                counter++;
                switch (counter % 4)
                {
                    case 0:
                        Console.Write("/");
                        counter = 0;
                        break;
                    case 1:
                        Console.Write("-");
                        break;
                    case 3:
                        Console.Write("\\");
                        break;
                    case 4:
                        Console.Write("|");
                        break;
                }
                Thread.Sleep(100);
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
            }
        }

        /// <summary>
        /// Clears any text in current line
        /// </summary>
        public static void clearln()
        {
            int cursorcurrentline = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, cursorcurrentline);
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