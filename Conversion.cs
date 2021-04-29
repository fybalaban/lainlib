/*
 *         LuddeToolset.Conversion
 * 
 *         LuddeToolset by ferityigitbalaban @ 2020
 *         https://www.github.com/fybalaban
 */

using System;

namespace LuddeToolset
{
    /// <summary>
    /// Convert objects in a safe, fast and reliable way. Currently supports object to integer, boolean, double, float operations.
    /// </summary>
    public static class Conversion
    {
        /// <summary>
        /// Converts object to integer.
        /// </summary>
        /// <param name="value">object to convert</param>
        /// <returns></returns>
        public static int ToInteger(this object value)
        {
            int result = -1;
            int.TryParse(value.ToString(), out result);
            return result;
        }

        /// <summary>
        /// Converts object to boolean.
        /// </summary>
        /// <param name="value">object to convert</param>
        /// <returns></returns>
        public static bool ToBoolean(this object value)
        {
            try
            {
                return Convert.ToBoolean(value);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Converts object to double.
        /// </summary>
        /// <param name="value">object to convert</param>
        /// <returns></returns>
        public static double ToDouble(this object value)
        {
            try
            {
                return Convert.ToDouble(value);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Converts object to float.
        /// </summary>
        /// <param name="value">object to convert</param>
        /// <returns></returns>
        public static float ToFloat(this object value)
        {
            try
            {
                return Convert.ToSingle(value);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Converts every item in T type array to integer type array. If supplied array was empty or null, returns null.
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static int[] ToIntegerArray<T>(this T[] array)
        {
            if (array != null && array.Length != 0)
            {
                int[] result = new int[array.Length];
                for (int i = 0; i < array.Length; i++)
                {
                    result[i] = ToInteger(array[i]);
                }
                return result;
            }
            return null;
        }

        /// <summary>
        /// Converts every item in T type array to string type array. If supplied array was empty or null, returns null.
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static string[] ToStringArray<T>(this T[] array)
        {
            if (array != null && array.Length != 0)
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
        /// Converts every item in T type array to double type array. If supplied array was empty or null, returns null.
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static double[] ToDoubleArray<T>(this T[] array)
        {
            if (array != null && array.Length != 0)
            {
                double[] result = new double[array.Length];
                for (int i = 0; i < array.Length; i++)
                {
                    result[i] = ToDouble(array[i]);
                }
                return result;
            }
            return null;
        }
    }
}
