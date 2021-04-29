/*
 *         LuddeToolset.TypeRecognition
 * 
 *         LuddeToolset by fybalaban @ 2020
 *         https://www.github.com/fybalaban
 */

namespace LuddeToolset
{
    /// <summary>
    /// Generalized type recognition for variables contained in string. Uses Regular Expressions and several methods to find types.
    /// </summary>
    public static partial class TypeRecognition
    {
        /// <summary>
        /// Types. Used for TypeRecognition.
        /// </summary>
        public enum Types
        {
            /// <summary>
            /// Any expression that is not a boolean, double or integer is a string.
            /// </summary>
            String = 0,
            /// <summary>
            /// Numbers but no dots.
            /// </summary>
            Integer = 1,
            /// <summary>
            /// "true" or "True" or "tRue" or "trUe" or "truE" or "TRue"... or "FALSE" is a boolean expression.
            /// </summary>
            Boolean = 2,
            /// <summary>
            /// Numbers but always with a dot.
            /// </summary>
            Double = 3,
            /// <summary>
            /// What's that? An error or something incomputable.
            /// </summary>
            Unknown = 4
        }
    }
}