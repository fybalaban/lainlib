using System;
using System.Security.Cryptography;
using System.Text;

namespace lainlib.Cryptography
{
    public static class Hash
    {
        public enum PseudorandomFunction { MD5, SHA128, SHA256, SHA384, SHA512 };

        public static byte[] SHA512(byte[] input)
        {
            if (input is null)
                throw new ArgumentNullException(nameof(input));
            if (input.Length is 0)
                throw new ArgumentOutOfRangeException(nameof(input));

            using (SHA512CryptoServiceProvider provider = new())
                return provider.ComputeHash(input);
        }

        public static string SHA512(string input) => string.IsNullOrWhiteSpace(input)
                ? throw new ArgumentNullException(nameof(input))
                : BitConverter.ToString(SHA512(Encoding.UTF8.GetBytes(input))).Replace("-", string.Empty).Replace("-", string.Empty);

        public static byte[] SHA384(byte[] input)
        {
            if (input is null)
                throw new ArgumentNullException(nameof(input));
            if (input.Length is 0)
                throw new ArgumentOutOfRangeException(nameof(input));

            using (SHA384CryptoServiceProvider provider = new())
                return provider.ComputeHash(input);
        }

        public static string SHA384(string input) => string.IsNullOrWhiteSpace(input)
                ? throw new ArgumentNullException(nameof(input))
                : BitConverter.ToString(SHA384(Encoding.UTF8.GetBytes(input))).Replace("-", string.Empty);

        public static byte[] SHA256(byte[] input)
        {
            if (input is null)
                throw new ArgumentNullException(nameof(input));
            if (input.Length is 0)
                throw new ArgumentOutOfRangeException(nameof(input));

            using (SHA256CryptoServiceProvider provider = new())
                return provider.ComputeHash(input);
        }

        public static string SHA256(string input) => string.IsNullOrWhiteSpace(input)
                ? throw new ArgumentNullException(nameof(input))
                : BitConverter.ToString(SHA256(Encoding.UTF8.GetBytes(input))).Replace("-", string.Empty);

        public static byte[] SHA128(byte[] input)
        {
            if (input is null)
                throw new ArgumentNullException(nameof(input));
            if (input.Length is 0)
                throw new ArgumentOutOfRangeException(nameof(input));

            using (SHA1CryptoServiceProvider provider = new())
                return provider.ComputeHash(input);
        }

        public static string SHA128(string input) => string.IsNullOrWhiteSpace(input)
                ? throw new ArgumentNullException(nameof(input))
                : BitConverter.ToString(SHA128(Encoding.UTF8.GetBytes(input))).Replace("-", string.Empty);

        public static byte[] MD5(byte[] input)
        {
            if (input is null)
                throw new ArgumentNullException(nameof(input));
            if (input.Length is 0)
                throw new ArgumentOutOfRangeException(nameof(input));

            using (MD5CryptoServiceProvider provider = new())
                return provider.ComputeHash(input);
        }

        public static string MD5(string input) => string.IsNullOrWhiteSpace(input)
                ? throw new ArgumentNullException(nameof(input))
                : BitConverter.ToString(MD5(Encoding.UTF8.GetBytes(input))).Replace("-", string.Empty);

        /// <summary>
        /// Performs the specified pseudorandom hash function on supplied input. Returns the hash value
        /// </summary>
        /// <param name="method"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public static byte[] PerformHash(PseudorandomFunction method, byte[] input) =>
            // Null check on input array is redundant as the next pseudorandom method will perform it when called
            method switch
            {
                PseudorandomFunction.MD5 => MD5(input),
                PseudorandomFunction.SHA128 => SHA128(input),
                PseudorandomFunction.SHA256 => SHA256(input),
                PseudorandomFunction.SHA384 => SHA384(input),
                PseudorandomFunction.SHA512 => SHA512(input),
                _ => null,
            };

        /// <summary>
        /// Returns the bit size of each pseudorandom function's hash array. You can divide the value by 8 to get byte sizes
        /// </summary>
        /// <param name="function">Pseudorandom function</param>
        /// <returns></returns>
        public static int GetSize(PseudorandomFunction function) =>
            function switch
            {
                PseudorandomFunction.MD5 => 128,
                PseudorandomFunction.SHA128 => 128,
                PseudorandomFunction.SHA256 => 256,
                PseudorandomFunction.SHA384 => 384,
                PseudorandomFunction.SHA512 => 512,
                _ => 0
            };
    }
}
