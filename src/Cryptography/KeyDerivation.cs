using System;
using System.Text;

namespace lainlib.Cryptography
{
    public static class KeyDerivation
    {

        public static byte[] PBKDF2(string passphrase, byte[] salt, Hash.PseudorandomFunction prf, int iteration)
        {
            if (string.IsNullOrEmpty(passphrase))
                throw new ArgumentNullException(nameof(passphrase));
            if (salt is null)
                throw new ArgumentNullException(nameof(salt));
            if (salt.Length is 0)
                throw new ArgumentOutOfRangeException(nameof(salt));
            if (iteration <= 0)
                throw new ArgumentOutOfRangeException(nameof(iteration));

            byte[] hashedPassphrase = Hash.PerformHash(prf, Encoding.UTF8.GetBytes(passphrase));

            byte[] data = new byte[salt.Length + hashedPassphrase.Length];
            hashedPassphrase.CopyTo(data, 0);
            salt.CopyTo(data, hashedPassphrase.Length);

            byte[] buf = Hash.PerformHash(prf, data);
            for (int i = 0; i < iteration - 1; i++)
                buf = Hash.PerformHash(prf, buf);

            return buf;
        }
    }
}
