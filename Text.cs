/*
 *         lainlib.String
 * 
 *         lainlib by fybalaban @ 2021
 *         https://www.github.com/fybalaban/lainlib
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lainlib
{
    public static class Text
    {
        /// <summary>
        /// Returns true if this string object is not null, whitespace or empty otherwise returns false.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool Valid(this string value)
        {
            return !string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// Returns true if value contains invalidChar.
        /// </summary>
        /// <param name="value">This value</param>
        /// <param name="invalidChar">Character to find</param>
        /// <returns></returns>
        public static bool ContainsCharacter(this string value, char invalidChar)
        {
            return value.Contains(invalidChar);
        }

        /// <summary>
        /// Returns true if value contains invalidChar.
        /// </summary>
        /// <param name="value">This value</param>
        /// <param name="invalidChar">Character to find</param>
        /// <returns></returns>
        public static bool ContainsCharacter(this string value, string invalidChar)
        {
            return value.Contains(invalidChar);
        }

        /// <summary>
        /// Returns true if value contains any characters from invalidChars array.
        /// </summary>
        /// <param name="value">This value</param>
        /// <param name="invalidChars">Characters to find</param>
        /// <returns></returns>
        public static bool ContainsCharacters(this string value, char[] invalidChars)
        {
            for (int i = 0; i < invalidChars.Length; i++)
            {
                if (value.ContainsCharacter(invalidChars[i]) == true)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Returns true if value contains any characters from invalidChars array.
        /// </summary>
        /// <param name="value">This value</param>
        /// <param name="invalidChars">Characters to find</param>
        /// <returns></returns>
        public static bool ContainsCharacters(this string value, string[] invalidChars)
        {
            for (int i = 0; i < invalidChars.Length; i++)
            {
                if (value.ContainsCharacter(invalidChars[i]) == true)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Returns a string containing DateTime information in dd-MMM-yyyyTHH-mm-ss.fff format.
        /// </summary>
        /// <returns></returns>
        public static string GetDateTimeNow()
        {
            return DateTime.Now.ToString("dd'-'MMM'-'yyyy'T'HH'-'mm'-'ss.fff");
        }

        /// <summary>
        /// Returns a string object in "tmp(random number between 100 -> 2147483647)" format that is suitable for using with I/O tools and file naming. 
        /// </summary>
        /// <param name="append"></param>
        /// <returns></returns>
        public static string GetTempString(string append = @"")
        {
            int i = new Random().Next(100, 2147483647);
            return string.Format("tmp{0}{1}", i, append);
        }

        /// <summary>
        /// Returns first N amount of characters in a string. If amount is not valid, returns string.
        /// </summary>
        /// <param name="string">string to return characters of</param>
        /// <param name="amount">amount of characters</param>
        /// <returns></returns>
        public static string GetFirstNCharacters(this string @string, uint amount)
        {
            if (@string.Valid() && amount >= 0 && amount < @string.Length)
            {
                StringBuilder stringBuilder = new(string.Empty);
                for (int i = 0; i < amount; i++)
                {
                    stringBuilder.Append(@string[i]);
                }
                return stringBuilder.ToString();
            }
            return @string;
        }

        /// <summary>
        /// Removes wrapping characters and returns what's left.
        /// Example: gets input string as "this is the string" => returns this is the string
        /// </summary>
        /// <param name="wrapper">Character that is wrapping text</param>
        /// <param name="removeFrom">String to remove wrappers of</param>
        /// <returns></returns>
        public static string RemoveWrappers(char wrapper, string removeFrom)
        {
            return removeFrom.Valid() && removeFrom.Contains(wrapper) ? removeFrom.Split(wrapper)[1] : removeFrom;
        }

        /// <summary>
        /// Removes character of string at given index (lowest index = 0). 
        /// </summary>
        /// <param name="value">String to remove character of</param>
        /// <param name="index">Index to remove</param>
        /// <returns></returns>
        public static string RemoveAt(this string value, int index)
        {
            return Valid(value) && (index >= 0) && (index < value.Length) ? value.Remove(index, 1) : value;
        }

        /// <summary>
        /// Removes last character of this string.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string RemoveLastCharacter(this string value)
        {
            return RemoveAt(value, value.Length - 1);
        }

        /// <summary>
        /// Removes last appearance of any string from a string.
        /// </summary>
        /// <param name="removeThis">String to remove appearance of</param>
        /// <param name="inHere">String to perform task on</param>
        /// <returns>Returns string.Empty if strings are not valid or inHere does not contain a string to remove.</returns>
        public static string RemoveLastAppearance(string removeThis, string inHere)
        {
            return removeThis.Valid() && inHere.Valid() && inHere.Contains(removeThis)
                ? inHere.Remove(inHere.LastIndexOf(removeThis), removeThis.Length)
                : string.Empty;
        }

        /// <summary>
        /// Removes all of the spaces in supplied string and returns the new version. Returns null if input string was not valid.
        /// </summary>
        /// <param name="removeFrom">String to remove spaces of</param>
        /// <returns></returns>
        public static string RemoveAllSpaces(this string removeFrom)
        {
            return removeFrom.Valid() && removeFrom.Contains(" ") ? removeFrom.Replace(" ", string.Empty) : null;
        }

        /// <summary>
        /// Tokenizes given text by space. Anything wrapped in character '"' will be counted as a word and not be splitted. 
        /// </summary>
        /// <param name="text">The input to tokenize</param>
        /// <returns>Returns List object containing tokens.</returns>
        public static List<string> Tokenize(string text)
        {
            return text.Split('"')
                .Select((element, index) => index % 2 == 0 // if even index
                ? element.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries) // split the item
                : new string[] { element }) // keep the entire item
                .SelectMany(element => element).ToList();
        }

        /// <summary>
        /// Appends every value contained in the array and returns the appended string.
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static string AppendAll(this string[] arr)
        {
            StringBuilder stringBuilder = new();
            for (int i = 0; i < arr.Length; i++)
            {
                stringBuilder.Append(arr[i]);
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Appends every value contained in the array and returns the appended string.
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static string AppendAll(this char[] arr)
        {
            StringBuilder stringBuilder = new();
            for (int i = 0; i < arr.Length; i++)
            {
                stringBuilder.Append(arr[i]);
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Appends every value contained in the array with given delimiter and returns the appended string.
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="delimiter">Delimiter to put between elements</param>
        /// <returns></returns>
        public static string AppendAll(this string[] arr, string delimiter)
        {
            StringBuilder stringBuilder = new();
            for (int i = 0; i < arr.Length; i++)
            {
                stringBuilder.Append(arr[i] + delimiter);
            }
            return stringBuilder.ToString().RemoveLastCharacter();
        }

        /// <summary>
        /// Appends every value contained in the array with given delimiter and returns the appended string.
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="delimiter">Delimiter to put between elements</param>
        /// <returns></returns>
        public static string AppendAll(this char[] arr, char delimiter)
        {
            StringBuilder stringBuilder = new();
            for (int i = 0; i < arr.Length; i++)
            {
                stringBuilder.Append(arr[i] + delimiter);
            }
            return stringBuilder.ToString().RemoveLastCharacter();
        }

        /// <summary>
        /// Wraps supplied text with supplied character. Returns wrapped text. 
        /// </summary>
        /// <param name="wrapper">Character to wrap the text</param>
        /// <param name="text">Text to be wrapped</param>
        /// <returns></returns>
        public static string WrapText(char wrapper, string text)
        {
            if (text.Valid())
            {
                text = text.Insert(0, wrapper.ToString());
                text = text.Insert(text.Length, wrapper.ToString());
            }
            return text;
        }

        /// <summary>
        /// Counts all occurances of given character in given text and returns count.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="character"></param>
        /// <returns></returns>
        public static int Count(string input, char character)
        {
            if (input.Valid() && input.Contains(character))
            {
                int count = 0;
                for (int i = 0; i < input.Length; i++)
                {
                    if (input[i] == character)
                    {
                        count++;
                    }
                }
                return count;
            }
            return 0;
        }
    }
}
