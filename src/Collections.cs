/*
 *         lainlib
 * 
 *         lainlib by fybalaban @ 2021
 *         https://www.github.com/fybalaban/lainlib
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace lainlib
{
    public static class Collections
    {
        /// <summary>
        /// Adds supplied element to last position of referenced array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="element"></param>
        public static void AddElementToArray<T>(ref T[] array, T element)
        {
            if (array is null || array?.Length is 0)
                throw new ArgumentNullException(nameof(array));

            T[] buffer = new T[array.Length + 1];
            array.CopyTo(buffer, 0);
            buffer[^1] = element;
            array = buffer;
        }

        /// <summary>
        /// Inserts supplied element at supplied index of referenced array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="element"></param>
        /// <param name="index"></param>
        public static void InsertElementToArray<T>(ref T[] array, T element, int index)
        {
            if (array is null || array?.Length is 0)
                throw new ArgumentNullException(nameof(array));
            if (index < 0 || index >= array.Length)
                throw new ArgumentOutOfRangeException(nameof(index));

            T[] buffer = new T[array.Length + 1];

            for (int i = 0; i < buffer.Length; i++)
                buffer[i] = i < index ? array[i] : i == index && !buffer[index].Equals(element) ? element : array[i - 1];

            array = buffer;

        }

        /// <summary>
        /// Removes element from supplied index of referenced array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="index"></param>
        public static void RemoveElementFromArray<T>(ref T[] array, int index)
        {
            if (array is null || array?.Length is 0)
                throw new ArgumentNullException(nameof(array));
            if (index < 0 || index >= array.Length)
                throw new ArgumentOutOfRangeException(nameof(index));

            T[] buffer = new T[array.Length - 1];

            for (int i = 0; i < array.Length; i++)
            {
                if (i < index)
                {
                    buffer[i] = array[i];
                }
                else if (i > index)
                {
                    buffer[i - 1] = array[i];
                }
            }

            array = buffer;
        }

        /// <summary>
        /// Returns index of element that contains the supplied object if it is contained in supplied IEnumerable object. If object is not found, returns -1
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="thisObject"></param>
        /// <returns></returns>
        public static int GetIndexOf<T>(this IEnumerable<T> enumerable, T thisObject)
        {
            if (enumerable is null || enumerable?.Any() is false)
                throw new ArgumentNullException(nameof(enumerable));

            for (int i = 0; i < enumerable.Count(); i++)
            {
                if (enumerable.ElementAt(i).Equals(thisObject))
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Returns index of element that contains the supplied object if it is contained in supplied array. If object is not found, returns -1
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="thisObject"></param>
        /// <returns></returns>
        public static int GetIndexOf<T>(this T[] array, T thisObject)
        {
            if (array is null || array?.Length is 0)
                throw new ArgumentNullException(nameof(array));

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].Equals(thisObject))
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Sorts referenced array in ascending (a -> z) order.
        /// </summary>
        /// <typeparam name="T">Please use string, char, int, double and float.</typeparam>
        /// <param name="array"></param>
        public static void SortAscending<T>(ref T[] array)
        {
            array = (from item in array orderby item ascending select item).ToArray();
        }

        /// <summary>
        /// Sorts referenced array in descending (z -> a) order.
        /// </summary>
        /// <typeparam name="T">Please use string, char, int, double and float.</typeparam>
        /// <param name="array"></param>
        public static void SortDescending<T>(ref T[] array)
        {
            array = (from item in array orderby item descending select item).ToArray();
        }

        /// <summary>
        /// Sorts this list in ascending (a -> z) order.
        /// </summary>
        /// <typeparam name="T">Please use string, char, int, double and float.</typeparam>
        /// <param name="list"></param>
        public static void SortAscending<T>(this List<T> list)
        {
            list = (from item in list orderby item ascending select item).ToList();
        }

        /// <summary>
        /// Sorts this list in descending (z -> a) order.
        /// </summary>
        /// <typeparam name="T">Please use string, char, int, double and float.</typeparam>
        /// <param name="list"></param>
        public static void SortDescending<T>(this List<T> list)
        {
            list = (from item in list orderby item descending select item).ToList();
        }

        /// <summary>
        /// Iterates through the collection object and checks if the object is filled with given value. Returns false if the object is null or the collection is empty.
        /// </summary>
        /// <typeparam name="T">Type of the collection and value</typeparam>
        /// <param name="collection">The collection to check</param>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static bool IsFilledWith<T>(this IEnumerable<T> collection, T value)
        {
            if (collection != null && collection.Any())
            {
                for (int i = 0; i < collection.Count(); i++)
                {
                    if (!collection.ElementAt(i).Equals(value))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Iterates through the array and checks if the array is filled with given value. Returns false if the array is null or empty.
        /// </summary>
        /// <typeparam name="T">Type of the array and value</typeparam>
        /// <param name="array">The array to check</param>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public static bool IsFilledWith<T>(this T[] array, T value)
        {
            if (array != null && array.Length > 0)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    if (!array[i].Equals(value))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Fills referenced array with supplied element.
        /// </summary>
        /// <param name="array">This referenced array to fill</param>
        /// <param name="element">Element object to fill the array</param>
        public static void Fill<T>(ref T[] array, T element)
        {
            if (array != null && array.Length != 0)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = element;
                }
            }
        }
    }
}