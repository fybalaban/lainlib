/*
 *         lainlib.Cryptography
 * 
 *         lainlib by fybalaban @ 2021
 *         https://www.github.com/fybalaban/lainlib
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace lainlib.Cryptography
{
    public static class Hash
    {
        private readonly static SHA512CryptoServiceProvider SHA512Provider = new();
        private readonly static SHA384CryptoServiceProvider SHA384Provider = new();
        private readonly static SHA256CryptoServiceProvider SHA256Provider = new();
        private readonly static SHA1CryptoServiceProvider SHA1Provider = new();
        private readonly static MD5CryptoServiceProvider MD5Provider = new();

        #region SHAxxx Hash Functions
        /// <summary>
        /// SHA512(value) in byte array
        /// </summary>
        /// <param name="value">The input to compute SHA512 hash</param>
        /// <returns></returns>
        public static byte[] GetSHA512ByteArray(string value)
        {
            return value.Valid() ? SHA512Provider.ComputeHash(Encoding.UTF8.GetBytes(value)) : (new byte[1] { 0 });
        }

        /// <summary>
        /// SHA512(value) in string
        /// </summary>
        /// <param name="value">The input to compute SHA512 hash</param>
        /// <returns></returns>
        public static string GetSHA512String(string value)
        {
            return value.Valid() ? BitConverter.ToString(GetSHA512ByteArray(value)) : string.Empty;
        }

        /// <summary>
        /// SHA384(value) in byte array
        /// </summary>
        /// <param name="value">The input to compute SHA384 hash</param>
        /// <returns></returns>
        public static byte[] GetSHA384ByteArray(string value)
        {
            return value.Valid() ? SHA384Provider.ComputeHash(Encoding.UTF8.GetBytes(value)) : (new byte[1] { 0 });
        }

        /// <summary>
        /// SHA384(value) in string
        /// </summary>
        /// <param name="value">The input to compute SHA384 hash</param>
        /// <returns></returns>
        public static string GetSHA384String(string value)
        {
            return value.Valid() ? BitConverter.ToString(GetSHA384ByteArray(value)) : string.Empty;
        }

        /// <summary>
        /// SHA256(value) in byte array
        /// </summary>
        /// <param name="value">The input to compute SHA256 hash</param>
        /// <returns></returns>
        public static byte[] GetSHA256ByteArray(string value)
        {
            return value.Valid() ? SHA256Provider.ComputeHash(Encoding.UTF8.GetBytes(value)) : (new byte[1] { 0 });
        }

        /// <summary>
        /// SHA256(value) in string
        /// </summary>
        /// <param name="value">The input to compute SHA256 hash</param>
        /// <returns></returns>
        public static string GetSHA256String(string value)
        {
            return value.Valid() ? BitConverter.ToString(GetSHA256ByteArray(value)) : string.Empty;
        }

        /// <summary>
        /// SHA1(value) in byte array
        /// </summary>
        /// <param name="value">The input to compute SHA1 hash</param>
        /// <returns></returns>
        public static byte[] GetSHA1ByteArray(string value)
        {
            return value.Valid() ? SHA1Provider.ComputeHash(Encoding.UTF8.GetBytes(value)) : (new byte[1] { 0 });
        }

        /// <summary>
        /// SHA1(value) in string
        /// </summary>
        /// <param name="value">The input to compute SHA1 hash</param>
        /// <returns></returns>
        public static string GetSHA1String(string value)
        {
            return value.Valid() ? BitConverter.ToString(GetSHA1ByteArray(value)) : string.Empty;
        }
        #endregion

        #region MD5
        /// <summary>
        /// MD5(value) in byte array.
        /// </summary>
        /// <param name="value">The input to compute MD5 hash</param>
        /// <returns></returns>
        public static byte[] GetMD5ByteArray(string value)
        {
            return value.Valid() ? MD5Provider.ComputeHash(Encoding.UTF8.GetBytes(value)) : (new byte[1] { 0 });
        }

        /// <summary>
        /// MD5(value) in string.
        /// </summary>
        /// <param name="value">The input to compute MD5 hash</param>
        /// <returns></returns>
        public static string GetMD5String(string value)
        {
            return value.Valid() ? BitConverter.ToString(GetMD5ByteArray(value)) : string.Empty;
        }
        #endregion
    }

    public static class Random
    {
        /// <summary>
        /// Initialized Random class.
        /// </summary>
        public static System.Random RandomGenerator = new();

        /// <summary>
        /// Returns random number.
        /// </summary>
        /// <returns></returns>
        public static int Number()
        {
            return RandomGenerator.Next();
        }

        /// <summary>
        /// Returns random number that is less than specified maximum.
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int Number(int max)
        {
            return RandomGenerator.Next(max);
        }

        /// <summary>
        /// Returns random number that is within the specified range.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int Number(int min, int max)
        {
            return min >= 0 ? RandomGenerator.Next(min, max) : -1;
        }
    }

    /// <summary>
    /// Provides safe methods to generate and store Initialization Vectors and Keys.
    /// </summary>
    public static class AESResourceSupplier
    {
        /// <summary>
        /// Generates a new and random Key in specified size.
        /// </summary>
        /// <param name="size">Size of key. (128, 192, 256)</param>
        /// <returns></returns>
        public static byte[] GetKey(int size)
        {
            AesManaged aes = new();
            if (aes.ValidKeySize(size))
            {
                aes.KeySize = size;
                aes.GenerateKey();
                return aes.Key;
            }
            throw new ArgumentException("Argument 'size' is not valid for use in AES encryption.");
        }

        /// <summary>
        /// Generates a new Initialization Vector.
        /// </summary>
        /// <returns></returns>
        public static byte[] GetIV()
        {
            AesManaged aes = new();
            aes.GenerateIV();
            return aes.IV;
        }

        /// <summary>
        /// Generates a .keystore file using specified key and specified initialization vector in specified path. <paramref name="path"/> should be in "...\filename" format. This method appends ".keystore" to path before saving.
        /// </summary>
        /// <param name="key">The key to store</param>
        /// <param name="keySize">Size of key</param>
        /// <param name="iv">The initialization vector to store</param>
        /// <param name="path">Full path with a filename to save</param>
        /// <returns>Returns a KeyStore object containing both the key and the initialization vector</returns>
        public static KeyStore GenerateKeyStore(byte[] key, int keySize, byte[] iv, string path)
        {
            try
            {
                StringBuilder keyBuilder = new();
                for (int j = 0; j < key.Length; j++)
                {
                    keyBuilder.Append(key[j].ToString() + " ");
                }
                string keyString = keyBuilder.ToString();

                StringBuilder ivBuilder = new();
                for (int j = 0; j < iv.Length; j++)
                {
                    ivBuilder.Append(iv[j].ToString() + " ");
                }
                string keyIV = ivBuilder.ToString();

                List<string> lines = new()
                {
                    keyString,
                    keySize.ToString(),
                    keyIV
                };

                IO.WriteLinesToFile(lines, string.Format(path+@".keystore"));

                return new KeyStore(key, keySize, iv);
            }
            catch (Exception e)
            {
                IO.CreateAndWriteErrorMessage(IO.ReturnDirectoryFromFullPath(path), e, "lainlib.Cryptography");
                return null;
            }
        }

        /// <summary>
        /// Generates a .keystore file with a key and an initialization vector in specified path. <paramref name="path"/> should be in "...\filename" format. This method appends ".keystore" to path before saving.
        /// </summary>
        /// <param name="keySize">Size of key</param>
        /// <param name="path">Full path with a filename to save</param>
        /// <returns>Returns a KeyStore object containing both the key and the initialization vector</returns>
        public static KeyStore GenerateKeyStore(int keySize, string path)
        {
            return GenerateKeyStore(GetKey(keySize), keySize, GetIV(), path);
        }

        /// <summary>
        /// Reads a .keystore file and returns a KeyStore object containing both the key and the initialization vector.
        /// </summary>
        /// <param name="filePath">Path to read from</param>
        /// <returns></returns>
        public static KeyStore ReadKeyStore(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Argument 'filePath' does not point to a valid file name!", filePath);
            }
            try
            {
                IO.ReadLinesFromFile(filePath, out IEnumerable<string> list);
                List<string> lines = list.ToList();
                string[] keyStringArray = lines[0].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                string keySize = lines[1];
                string[] ivStringArray = lines[2].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                byte[] key = new byte[32];
                byte[] iv = new byte[16];

                for (int i = 0; i < keyStringArray.Length; i++)
                {
                    key[i] = Convert.ToByte(keyStringArray[i]);
                }
                for (int i = 0; i < ivStringArray.Length; i++)
                {
                    iv[i] = Convert.ToByte(ivStringArray[i]);
                }

                return new KeyStore(key, keySize.ToInteger(), iv);
            }
            catch (Exception e)
            {
                IO.CreateAndWriteErrorMessage(IO.ReturnDirectoryFromFullPath(filePath), e, "lainlib.Cryptography");
                return null;
            }
        }

        /// <summary>
        /// Checks if given key size is valid for use with AES algorithm.
        /// </summary>
        /// <param name="size">The size</param>
        /// <returns></returns>
        public static bool IsValidKeySize(int size)
        {
            AesManaged aes = new();
            return aes.ValidKeySize(size);
        }
    }
}