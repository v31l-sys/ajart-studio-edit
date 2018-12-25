using System;
using System.Linq;
using System.Text;
using Ionic.Zlib;
using System.Security.Cryptography;
using System.IO;

namespace ajart_decoder
{
    static class Decoder
    {
        private static string current_uuid = "";
        private static byte[] _keybytes_ = new byte[0];
        private static byte[] _ivbytes_ = new byte[0];
        public static string GetUUID() { return current_uuid; }

        //AMF3 Parser Implemented in C++/CLI
        static AMF3SpecCLI.AMFParser AMFParser = new AMF3SpecCLI.AMFParser();

        public static bool ParseUUID(string uuid_)
        {
            if (uuid_.Length == 36)
            {
                current_uuid = uuid_;

                string _key_ = "";
                string _iv_ = "";

                int _counter_ = 0;
                while (_key_.Length < 16)
                {
                    _key_ += uuid_.ElementAt(_counter_++);
                    _iv_ += uuid_.ElementAt(_counter_++);
                }

                _keybytes_ = Encoding.Default.GetBytes(_key_);
                _ivbytes_ = Encoding.Default.GetBytes(_iv_);

                return true;
            }
            else
                return false;
        }

        //Simple generic encryption algorithm
        private static byte[] SimpleEncrypt(SymmetricAlgorithm algorithm, CipherMode cipherMode, byte[] key, byte[] iv, byte[] bytes)
        {
            algorithm.Mode = cipherMode;
            algorithm.Padding = PaddingMode.Zeros;
            algorithm.Key = key;
            algorithm.IV = iv;

            using (var encryptor = algorithm.CreateEncryptor())
            {
                return encryptor.TransformFinalBlock(bytes, 0, bytes.Length);
            }
        }

        //Simple generic decryption algorithm
        private static byte[] SimpleDecrypt(SymmetricAlgorithm algorithm, CipherMode cipherMode, byte[] key, byte[] iv, byte[] bytes)
        {
            algorithm.Mode = cipherMode;
            algorithm.Padding = PaddingMode.Zeros;
            algorithm.Key = key;
            algorithm.IV = iv;

            using (var encryptor = algorithm.CreateDecryptor())
            {
                return encryptor.TransformFinalBlock(bytes, 0, bytes.Length);
            }
        }

        public static bool Decode(string path)
        {
            Console.WriteLine("Decrypting File...");
            //ENC -> DECOMP -> AMF -> PNG
            byte[] byteData = new byte[0];
            //AES encryption scheme is used by AJ for these files
            byteData = SimpleDecrypt(new RijndaelManaged(), CipherMode.CBC, _keybytes_, _ivbytes_, File.ReadAllBytes(path));
            byte[] buffuncompress = new byte[1024];

            Console.WriteLine("Decompressing File...");
            try
            {
                using (Stream decompstream = new MemoryStream(byteData))
                {
                    using (ZlibStream decompressionStream = new ZlibStream(decompstream, Ionic.Zlib.CompressionMode.Decompress))
                    {
                        var resultStream = new MemoryStream();
                        int read;

                        while ((read = decompressionStream.Read(buffuncompress, 0, buffuncompress.Length)) > 0)
                        {
                            resultStream.Write(buffuncompress, 0, read);
                        }

                        byteData = resultStream.ToArray();
                    }
                }
            }
            catch (Ionic.Zlib.ZlibException)
            {
                Console.WriteLine("Failed Decompression...");
                Console.WriteLine("Do you have the right account UUID?");
                return false;
            }

            string input = "";
            Console.Write("AMF data gathered, save? (Y/n): ");
            input = Console.ReadLine().ToUpper();
            if(input == "Y")
                File.WriteAllBytes(path.Substring(0, path.IndexOf('.')) + ".amf", byteData);

            byteData = AMFParser.deserialize(byteData);

            Console.Write("Image data gathered, save? (Y/n): ");
            input = Console.ReadLine().ToUpper();
            if (input == "Y")
                File.WriteAllBytes(path.Substring(0, path.IndexOf('.')) + ".png", byteData);

            return true;
        }
    }
}
