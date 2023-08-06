﻿using System.Security.Cryptography;

internal class Program
{
    private static void Main(string[] args)
    {
        string textoOriginal = "Text to encrypt";

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.KeySize = 256;
            aesAlg.GenerateKey();
            aesAlg.GenerateIV();

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(textoOriginal);
                    }
                }

                byte[] EncryptrdText = msEncrypt.ToArray();
                string EncValue = Convert.ToBase64String(EncryptrdText);
                string Key = Convert.ToBase64String(aesAlg.Key);
                string IV = Convert.ToBase64String(aesAlg.IV);
                Console.WriteLine("Encripted Value: " + EncValue);
                Console.WriteLine("Generated Key: " + Key);
                Console.WriteLine("IV: " + IV);
            }
        }

    }
}