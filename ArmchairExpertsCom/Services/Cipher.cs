﻿using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ArmchairExpertsCom.Services
{
    public static class Cipher
    {
        public static string GetKey(string password)
        {
            byte[] pwd = Encoding.Unicode.GetBytes(password);
            byte[] salt = CreateRandomSalt(7);

            // Create a TripleDESCryptoServiceProvider object.
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

            try
            {
                // Create a PasswordDeriveBytes object and then create
                // a TripleDES key from the password and salt.
                PasswordDeriveBytes pdb = new PasswordDeriveBytes(pwd, salt);


                // Create the key and set it to the Key property
                // of the TripleDESCryptoServiceProvider object.
                // This example uses the SHA1 algorithm.
                // Due to collision problems with SHA1, Microsoft recommends SHA256 or better.
                tdes.Key = pdb.CryptDeriveKey("TripleDES", "SHA1", 192, tdes.IV);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return Encoding.Default.GetString(tdes.Key);
        }
        
        private static byte[] CreateRandomSalt(int length)
        {
            // Create a buffer
            byte[] randBytes;

            if (length >= 1)
            {
                randBytes = new byte[length];
            }
            else
            {
                randBytes = new byte[1];
            }

            // Create a new RNGCryptoServiceProvider.
            RNGCryptoServiceProvider rand = new RNGCryptoServiceProvider();

            // Fill the buffer with random bytes.
            rand.GetBytes(randBytes);

            // return the bytes.
            return randBytes;
        }

        private static void ClearBytes(IList<byte> buffer)
        {
            // Check arguments.
            if (buffer == null)
            {
                throw new ArgumentException("buffer");
            }

            // Set each byte in the buffer to 0.
            for (int x = 0; x < buffer.Count; x++)
            {
                buffer[x] = 0;
            }
        }
    }
}