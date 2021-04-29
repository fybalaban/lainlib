/*
 *         LuddeToolset.Cryptography
 * 
 *         LuddeToolset by fybalaban @ 2020
 *         https://www.github.com/fybalaban
 */

namespace LuddeToolset.Cryptography
{
        /// <summary>
        /// Used to store a symmetric cryptography algorithm key and a initialization vector.
        /// </summary>
        public class KeyStore
        {
            /// <summary>
            /// The key
            /// </summary>
            public byte[] Key { get; set; }
            /// <summary>
            /// Size of the key
            /// </summary>
            public int KeySize { get; set; }
            /// <summary>
            /// The initialization vector
            /// </summary>
            public byte[] IV { get; set; }

            /// <summary>
            /// Initializes a empty KeyStore object.
            /// </summary>
            public KeyStore()
            {

            }

            /// <summary>
            /// Initialize a KeyStore object.
            /// </summary>
            /// <param name="key">The key</param>
            /// <param name="size">Size of the key</param>
            /// <param name="iv">The initialization vector</param>
            public KeyStore(byte[] key, int size, byte[] iv)
            {
                Key = key;
                KeySize = size;
                IV = iv;
            }
        }
}