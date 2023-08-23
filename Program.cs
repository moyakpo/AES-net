using System.Security.Cryptography;

internal class Program
{
    private static void Main(string[] args)
    {
        string sampleText = "Text to encrypt";
        string textToEncypt = args.Length > 0 ? args[0] : sampleText;

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
                        swEncrypt.Write(textToEncypt);
                    }
                }

                byte[] EncryptrdText = msEncrypt.ToArray();
                string EncValue = Convert.ToBase64String(EncryptrdText);
                string Key = Convert.ToBase64String(aesAlg.Key);
                string IV = Convert.ToBase64String(aesAlg.IV);
                
                Console.WriteLine("");
                Console.WriteLine("Plain value: {0}", textToEncypt);
                Console.WriteLine("Generated Key (Key1): {0}", Key);
                Console.WriteLine("IV (Key2): {0}", IV);
                Console.WriteLine("Encripted Value (Key3): {0}", EncValue);
            }
        }

    }
}