/*
 *         lainlib
 * 
 *         lainlib by fybalaban @ 2021
 *         https://www.github.com/fybalaban/lainlib
 */

using System;

namespace lainlib
{
    /// <summary>
    /// Convert objects in a safe, fast and reliable way. Currently supports object to integer, boolean, double, float operations.
    /// </summary>
    public static class Conversion
    {
        #region Object Conversion
        /// <summary>
        /// Converts numerical value of object to 32-bit signed integer
        /// </summary>
        /// <param name="value">Object to convert</param>
        /// <returns></returns>
        public static int ToInteger(this object value) => Convert.ToInt32(value);

        /// <summary>
        /// Converts string representation of logical value to boolean value
        /// </summary>
        /// <param name="value">Object to convert</param>
        /// <returns></returns>
        public static bool ToBoolean(this string value)
        {
            return (value is not null && value.ToLower() is "true") || (value is not null && value.ToLower() is "false"
                ? false
                : throw new InvalidCastException($"{nameof(value)} holds value '{value}' which does not express a logical value"));
        }

        /// <summary>
        /// Converts numerical representation of logical value to boolean value
        /// </summary>
        /// <param name="value">Object to convert</param>
        /// <returns></returns>
        public static bool ToBoolean(this int value)
        {
            return value > 0;
        }

        /// <summary>
        /// Converts the value of object to double-precision number
        /// </summary>
        /// <param name="value">Object to convert</param>
        /// <returns></returns>
        public static double ToDouble(this object value) => Convert.ToDouble(value);

        /// <summary>
        /// Converts numerical value of object to single-precision number
        /// </summary>
        /// <param name="value">Object to convert</param>
        /// <returns></returns>
        public static float ToFloat(this object value) => Convert.ToSingle(value);
        #endregion

        #region Array Conversion
        /// <summary>
        /// Converts a type T array to integer array. If supplied array was empty or null, returns null.
        /// </summary>
        /// <param name="array">Array to convert</param>
        /// <returns></returns>
        public static int[] ToIntegerArray<T>(this T[] array)
        {
            if (array is not null && array.Length is not 0)
            {
                int[] result = new int[array.Length];
                for (int i = 0; i < array.Length; i++)
                {
                    result[i] = array[i] is not null ? array[i].ToInteger() : throw new InvalidCastException("Cannot convert array with null element to integer array");
                }
                return result;
            }
            return null;
        }

        /// <summary>
        /// Converts an array of string objects logical values to boolean values
        /// </summary>
        /// <param name="array">An array of string objects to convert</param>
        /// <returns></returns>
        public static bool[] ToBooleanArray(this string[] array)
        {
            if (array is not null && array.Length is not 0)
            {
                bool[] result = new bool[array.Length];
                for (int i = 0; i < array.Length; i++)
                {
                    result[i] = array[i] is not null ? array[i].ToBoolean() : throw new InvalidCastException("Cannot convert array with null element to integer array");
                }
                return result;
            }
            return null;
        }

        /// <summary>
        /// Converts a type T array to string array. If supplied array was empty or null, returns null
        /// </summary>
        /// <param name="array">Array of type T objects to convert</param>
        /// <returns></returns>
        public static string[] ToStringArray<T>(this T[] array)
        {
            if (array is not null && array.Length is not 0)
            {
                string[] result = new string[array.Length];
                for (int i = 0; i < array.Length; i++)
                {
                    result[i] = array[i].ToString();
                }
                return result;
            }
            return null;
        }

        /// <summary>
        /// Converts a type T array to double array. If supplied array was empty or null, returns null
        /// </summary>
        /// <param name="array">Array of type T objects to convert</param>
        /// <returns></returns>
        public static double[] ToDoubleArray<T>(this T[] array)
        {
            if (array is not null && array.Length is not 0)
            {
                double[] result = new double[array.Length];
                for (int i = 0; i < array.Length; i++)
                {
                    result[i] = array[i] is not null ? array[i].ToDouble() : throw new InvalidCastException("Cannot convert array with null element to double array");
                }
                return result;
            }
            return null;
        }

        /// <summary>
        /// Converts a type T array to float array. If supplied array was empty or null, returns null
        /// </summary>
        /// <param name="array">Array of type T objects to convert</param>
        /// <returns></returns>
        public static float[] ToFloatArray<T>(this T[] array)
        {
            if (array is not null && array.Length is not 0)
            {
                float[] result = new float[array.Length];
                for (int i = 0; i < array.Length; i++)
                {
                    result[i] = array[i] is not null ? array[i].ToFloat() : throw new InvalidCastException("Cannot convert array with null element to float array");
                }
                return result;
            }
            return null;
        }
        #endregion
    }
}