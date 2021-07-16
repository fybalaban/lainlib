using System.Collections.Generic;
using System.Linq;

namespace lainlib
{
    /// <summary>
    /// 
    /// </summary>
    public static class Collections
    {
        /// <summary>
        /// Returns index of element in referenced array.
        /// </summary>
        /// <typeparam name="T">The sky is the limit ;)</typeparam>
        /// <param name="arr">Array to search in</param>
        /// <param name="element">Element to search for</param>
        /// <returns>Returns -1 if something is wrong.</returns>
        public static int GetIndexOfElement<T>(T[] arr, T element)
        {
            if (arr.Length > 0 && element != null)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    if (element.Equals(arr[i]))
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// Remove anything from any index of referenced array.
        /// </summary>
        /// <typeparam name="T">The sky is the limit ;)</typeparam>
        /// <param name="arr"></param>
        /// <param name="index"></param>
        public static void RemoveAt<T>(ref T[] arr, int index)
        {
            if (index >= 0 && index < arr.Length)
            {
                while (index < arr.Length - 1)
                {
                    arr[index] = arr[index + 1];
                    index++;
                }
            }
        }

        /// <summary>
        /// Returns true if this enumerable object of type T contains object of type T. If the enumerable object is null or does not contain any element, returns false.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="thisObject"></param>
        /// <returns></returns>
        public static bool Contains<T>(this IEnumerable<T> enumerable, T thisObject)
        {
            if (enumerable != null && enumerable.Any())
            {
                for (int i = 0; i < enumerable.Count(); i++)
                {
                    if (enumerable.ElementAt(i).Equals(thisObject))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Returns true if this array of type T contains object of type T. If the array is null or does not contain any element, returns false.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="thisObject"></param>
        /// <returns></returns>
        public static bool Contains<T>(this T[] array, T thisObject)
        {
            if (array != null && array.Length > 0)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i].Equals(thisObject))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Returns true if this enumerable object of type T contains object of type T. If the enumerable object is null or does not contain any element, returns false.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="thisObject"></param>
        /// <returns></returns>
        public static bool Contains<T>(this IEnumerable<T> enumerable, T thisObject, out int indexOfObject)
        {
            indexOfObject = -1;
            if (enumerable != null && enumerable.Any())
            {
                for (int i = 0; i < enumerable.Count(); i++)
                {
                    if (enumerable.ElementAt(i).Equals(thisObject))
                    {
                        indexOfObject = i;
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Returns true if this array of type T contains object of type T. If the array is null or does not contain any element, returns false.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="thisObject"></param>
        /// <returns></returns>
        public static bool Contains<T>(this T[] array, T thisObject, out int indexOfObject)
        {
            indexOfObject = -1;
            if (array != null && array.Length > 0)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i].Equals(thisObject))
                    {
                        indexOfObject = i;
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Iterates through the string array and looks for an element that contains the supplied value. When an element that contains the value is found, method returns <see langword="true"/>, and <see langword="out"/> parameter contains the index of the element.
        /// If the array is null or does not contain any element, returns false.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="lookup"></param>
        /// <param name="indexOfContainingElement"></param>
        /// <returns></returns>
        public static bool AnyElementContains(this string[] array, string value, out int indexOfContainingElement)
        {
            indexOfContainingElement = -1;
            if (array != null && array.Length > 0)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i].Contains(value))
                    {
                        indexOfContainingElement = i;
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Iterates through the string enumerable and looks for an element that contains the supplied value. When an element that contains the value is found, method returns <see langword="true"/>, and <see langword="out"/> parameter contains the index of the element.
        /// If the enumerable object is null or does not contain any element, returns false.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="lookup"></param>
        /// <param name="indexOfContainingElement"></param>
        /// <returns></returns>
        public static bool AnyElementContains(this IEnumerable<string> enumerable, string value, out int indexOfContainingElement)
        {
            indexOfContainingElement = -1;
            if (enumerable != null && enumerable.Any())
            {
                for (int i = 0; i < enumerable.Count(); i++)
                {
                    if (enumerable.ElementAt(i).Contains(value))
                    {
                        indexOfContainingElement = i;
                        return true;
                    }
                }
            }
            return false;
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
