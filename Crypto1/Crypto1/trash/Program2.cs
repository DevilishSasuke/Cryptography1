/*using System.Drawing;
using System.Numerics;

namespace Cryptography
{
    public class CryptoRSA
    {
        private BigInteger p;
        public BigInteger P { get { return p; } }
        private BigInteger q;
        public BigInteger Q { get { return q; }}
        private BigInteger n;
        public BigInteger N { get { return n; } }
        private BigInteger e;
        public BigInteger E { get { return e; } }
        private BigInteger d;
        public BigInteger D { get { return d; } }

        public void GenerateNewKeys()
        {
            p = randPrime();
            q = randPrime();

            n = p * q;

            BigInteger phi = EulerFunc(p, q);
            e = GetE();


        }

        // https://habr.com/ru/articles/470159/
        public BigInteger randPrime()
        {
            return 2;
        }

        public bool IsLikelyPrime(BigInteger a)
        {
            return true;
        }

        public BigInteger EulerFunc(BigInteger p, BigInteger q)
        {
            return (p - 1) * (q - 1);
        }

        private BigInteger GetE()
        {
            string answer = "";
            while (answer != "1" || answer != "2")
            {
                Console.WriteLine("1. Ввести e вручную");
                Console.WriteLine("2. Вычислить e");
                answer = Console.ReadLine();
            }

            if (answer == "1")
                return GetNumberFromConsole();
            else
                return CalculateE();
        }

        private BigInteger GetNumberFromConsole()
        {
            return 0;
            BigInteger result;
            Console.WriteLine("Введите значение e: ");
            while (!BigInteger.TryParse(Console.ReadLine(), out result))
            {

            }
        }

        private BigInteger CalculateE()
        {
            return 0;
        }

        public int[] sieve;
        public void MakeSieve()
        {
            if (this.sieve != null) 
                return;

            var sieve = new int[5000];
            for (int i = 0; i < sieve.Length; ++i)
            {
                sieve[i] = i;
            }

            for (int j = 2; j < sieve.Length; ++j)
            {
                if (sieve[j] == 0) continue;
                for (int k = j * j; k < sieve.Length; k += j)
                    sieve[k] = 0;
            }

            var primeList = new List<int>();
            for (int i = 2; i < sieve.Length; ++i)
                if (sieve[i] != 0)
                    primeList.Add(sieve[i]);

            this.sieve = primeList.ToArray();
        }
    }

    public class Program
    {
        public static void Main()
        {
            var array = EratosthenesSieve.GetSieve();
        }

        public bool IsLikelyPrime(int number)
        {

            return false;
        }
    }
}

public static class EratosthenesSieve
{
    private static int previousN;
    private static int[] sieve;
    public static int[] GetSieve(int n = 5000)
    {
        if (n < 1)
            n = 5000;
        if (sieve != null && previousN == n)
        {
            Console.WriteLine("skipped");
            return sieve;
        }
        previousN = n;

        var methodSieve = new int[n];
        for (int i = 0; i < methodSieve.Length; ++i)
        {
            methodSieve[i] = i;
        }

        for (int j = 2; j < methodSieve.Length; ++j)
        {
            if (methodSieve[j] == 0) continue;
            for (int k = j * j; k < methodSieve.Length; k += j)
                methodSieve[k] = 0;
        }

        var primeList = new List<int>();
        for (int i = 2; i < methodSieve.Length; ++i)
            if (methodSieve[i] != 0)
                primeList.Add(methodSieve[i]);

        sieve = primeList.ToArray();
        return sieve;
    }
}

*/