/*
 *         lainlib.TypeRecognition
 * 
 *         lainlib by fybalaban @ 2021
 *         https://www.github.com/fybalaban/lainlib
 */

using System.Text.RegularExpressions;

namespace lainlib
{
    /// <summary>
    /// Generalized type recognition for variables contained in string. Uses Regular Expressions and several methods to find types.
    /// </summary>
    public static partial class TypeRecognition
    {
        private static readonly Regex Integers = new("/^([+-]?[1-9]\\d*|0)$", RegexOptions.Compiled);
        private static readonly Regex Doubles = new("[+-]?([0-9]*[.])?[0-9]+", RegexOptions.Compiled);
        private static readonly Regex Strings = new("[a-zA-Z]", RegexOptions.Compiled);
        private static readonly Regex SChartr = new(@"[ ! ^ # £ $ + \- _ \| < > : ; , ~ ¨ ` ´ ' % & / ( ) = ? * \\ } \] \[ { ]", RegexOptions.Compiled);
        private static readonly Regex SCharEx = new("[ \" ]", RegexOptions.Compiled);

        /// <summary>
        /// Returns true if given string contains a boolean expression. ("true" or "false")
        /// </summary>
        /// <param name="value">The input to find type of</param>
        /// <returns></returns>
        public static bool IsBoolean(this string value)
        {
            return value.ToLower().Trim() == "true" || value.ToLower().Trim() == "false";
        }

        /// <summary>
        /// Returns true if given string contains a string that IS NOT a boolean, an integer or a double expression.
        /// </summary>
        /// <param name="value">The input to find type of</param>
        /// <returns></returns>
        public static bool IsString(this string value)
        {
            string x = value.ToLower().Trim();
            return !IsBoolean(x) && (Strings.IsMatch(x) || SChartr.IsMatch(x) || SCharEx.IsMatch(x));
        }

        /// <summary>
        /// Returns the 'Type' of given value.
        /// </summary>
        /// <param name="value">The value to find type of</param>
        /// <returns>Any enum from lainlib.TypeRecognition.Types</returns>
        public static Types FindType(this string value)
        {
            if (value.Valid())
            {
                string x = value.ToLower().Trim();
                return x.IsBoolean()
                    ? Types.Boolean
                    : Doubles.IsMatch(x) && !x.IsString()
                        ? !x.Contains(".") ? Types.Integer : Types.Double
                        : x.IsString() ? Types.String : Types.Unknown;
            }
            return Types.Unknown;
        }
    }
}
