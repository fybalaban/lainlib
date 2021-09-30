namespace lainlib.Cryptography
{
    public static class Random
    {
        internal static System.Random RandomNumberGenerator = new();

        /// <summary>
        /// Returns random number.
        /// </summary>
        /// <returns></returns>
        public static int Number() => RandomNumberGenerator.Next();

        /// <summary>
        /// Returns random number that is less than specified maximum.
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int Number(int max) => RandomNumberGenerator.Next(max);

        /// <summary>
        /// Returns random number that is within the specified range.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int Number(int min, int max) => min >= 0 ? RandomNumberGenerator.Next(min, max) : -1;
    }
}
