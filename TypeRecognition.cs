/*
 *         LuddeToolset.TypeRecognition
 * 
 *         LuddeToolset by fybalaban @ 2020
 *         https://www.github.com/fybalaban
 */

using System.Text.RegularExpressions;

namespace LuddeToolset
{
    /// <summary>
    /// Generalized type recognition for variables contained in string. Uses Regular Expressions and several methods to find types.
    /// </summary>
    public static partial class TypeRecognition
    {
        private readonly static Regex Integers = new Regex("/^([+-]?[1-9]\\d*|0)$", RegexOptions.Compiled);
        private readonly static Regex Doubles = new Regex("[+-]?([0-9]*[.])?[0-9]+", RegexOptions.Compiled);
        private readonly static Regex Strings = new Regex("[a-zA-Z]", RegexOptions.Compiled);
        private readonly static Regex SChartr = new Regex(@"[ ! ^ # £ $ + \- _ \| < > : ; , ~ ¨ ` ´ ' % & / ( ) = ? * \\ } \] \[ { ]", RegexOptions.Compiled);
        private readonly static Regex SCharEx = new Regex("[ \" ]", RegexOptions.Compiled);

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
        /// <returns>Any enum from LuddeToolset.TypeRecognition.Types</returns>
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
