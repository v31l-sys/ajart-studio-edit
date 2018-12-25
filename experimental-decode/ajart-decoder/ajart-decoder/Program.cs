using System;

namespace ajart_decoder
{
    class Program
    {
        public static string ChangeUUID()
        {
            Console.Write("Enter a valid UUID: ");
            return Console.ReadLine();
        }

        static void Main(string[] args)
        {
            string uuid = ChangeUUID();
            while(Decoder.ParseUUID(uuid) == false)
                uuid = ChangeUUID();

            while (true)
            {
                Console.Write("UUID is: {0}\nUse this UUID? (Y/n): ", Decoder.GetUUID());
                if (Console.ReadLine().ToUpper() == "N")
                {
                    uuid = ChangeUUID();
                    while (Decoder.ParseUUID(uuid) == false)
                        uuid = ChangeUUID();
                }

                Console.Write("Enter file path + file to decode: ");
                string filePath = Console.ReadLine();
                if(Decoder.Decode(filePath) == false)
                    Console.Write("Decoding failed..\nContinue? (Y/n): ");
                else
                    Console.Write("Decoding successful!..\nContinue? (Y/n): ");

                if (Console.ReadLine().ToUpper() == "N")
                    break;
            }
        }
    }
}
