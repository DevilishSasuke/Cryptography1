using System;
using System.Numerics;

namespace Cryptography
{
    public class Program
    {
        public static void Main()
        {
            CryptoRSA unit = new CryptoRSA();
            string answer = "";
            bool isGenerated = false;

            while (true)
            {
                Console.WriteLine("\n-----------------\n");
                Console.WriteLine("Выберите опцию:");
                Console.WriteLine("1.Сгенерировать новые ключи");
                Console.WriteLine("2.Зашифровать текст");
                Console.WriteLine("3.Расшифровать текст");
                Console.WriteLine("4. Выход");

                do answer = Console.ReadLine();
                while (answer != "1" && answer != "2" &&
                    answer != "3" && answer != "4");

                switch (answer)
                {
                    case "1":
                        isGenerated = true;
                        unit.GenerateNewKeys();
                        Console.WriteLine(String.Format("\np: {0}; \nq: {1}; " +
                            "\ne: {2}; \nd: {3}; \nphi: {4}",
                            unit.p, unit.q, 
                            unit.e, unit.d, unit.phi));
                        break;
                    case "2":
                        if (!isGenerated)
                        {
                            Console.WriteLine("\nСначала сгенерируйте ключи.");
                            continue;
                        }
                        Console.WriteLine("\nВведите текст для шифрования: ");
                        var encrypted = unit.EncryptText(GetNumberFromConsole());
                        Console.WriteLine("Encrypted text:\n" + encrypted);
                        break;
                    case "3":
                        if (!isGenerated)
                        {
                            Console.WriteLine("\nСначала сгенерируйте ключи.");
                            continue;
                        }
                        Console.WriteLine("\nВведите текст для расшифровки: ");
                        var decrypted = unit.DecryptText(GetNumberFromConsole());
                        Console.WriteLine("Decrypted text:\n" + decrypted);
                        break;
                    case "4":
                        return;
                }
            }
        }


        public static string ChooseE()
        {
            string answer = "";
            while (answer != "1" && answer != "2")
            {
                Console.WriteLine("\n1. Ввести e вручную");
                Console.WriteLine("2. Вычислить e");
                answer = Console.ReadLine();
            }

            if (answer == "1")
                Console.WriteLine("Введите значение e: ");

            return answer;
        }

        public static BigInteger GetNumberFromConsole()
        {
            BigInteger result;

            while (!BigInteger.TryParse(Console.ReadLine(), out result)) { }
            return result;
        }
    }

    
}