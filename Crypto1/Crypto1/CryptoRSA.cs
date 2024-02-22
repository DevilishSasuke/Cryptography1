using System.Numerics;
using System.Security.AccessControl;

namespace Cryptography
{
    public class CryptoRSA
    {
        public BigInteger p { get; private set; } = BigInteger.Zero;
        public BigInteger q { get; private set; } = BigInteger.Zero;
        public BigInteger N { get; private set; } = BigInteger.Zero;
        public BigInteger e { get; private set; } = BigInteger.Zero;
        public BigInteger d { get; private set; } = BigInteger.Zero;
        public BigInteger phi { get; private set; } = BigInteger.Zero;
        private readonly PrimeGenerator rand = new PrimeGenerator();

        public void GenerateNewKeys()
        {
            p = rand.GeneratePrime();
            q = rand.GeneratePrime();

            N = p * q;

            phi = EulerFunc(p, q);
            e = GetE();
            d = (EuclidExtended(e, phi).Item2 + phi) % phi;
        }

        public BigInteger EncryptText(BigInteger text) => N > 0 ? 
            ModExp(text, e, N): 
            throw new Exception("Keys must be generated");

        public BigInteger DecryptText(BigInteger text) => N > 0 ? 
            ModExp(text, d, N): 
            throw new Exception("Keys must be generated");

        public (BigInteger, BigInteger) GetPublicKey() => (e, N);
        public (BigInteger, BigInteger) GetPrivateKey() => (d, N);

        public static BigInteger ModExp(BigInteger a, BigInteger b, BigInteger n)
        {
            var c = a;
            BigInteger d = 1;
            while (b > 0)
            {
                if (b % 2 == 1)
                    d = (d * c) % n;
                b = b / 2;
                c = (c * c) % n;
            }

            return d;
        }

        public BigInteger EulerFunc(BigInteger p, BigInteger q) => (p - 1) * (q - 1);

        public static (BigInteger, BigInteger, BigInteger) EuclidExtended(BigInteger a, BigInteger b)
        {
            if (b == 0)
                return (a, 1, 0);

            BigInteger d, x, y, x1, y1;
            (d, x1, y1) = EuclidExtended(b, a % b);
            x = y1;
            y = x1 - a / b * y1;

            return (d, x, y);
        }
        
        public static BigInteger Euclid(BigInteger a, BigInteger b)
        {
            BigInteger t;
            while (b > 0)
            {
                t = b;
                b = a % b;
                a = t;
            }

            return a;
        }

        public BigInteger GetRelativelyPrime(BigInteger a)
        {
            BigInteger b;

            while (true)
            {
                b = rand.GeneratePrime();
                if (b >= a && b <= 2) continue;

                for (b = b % a; b < a; ++b)
                {
                    if (GCD(a, b) == 1)
                        return b;
                }
            }
        }

        private static BigInteger GCD(BigInteger num1, BigInteger num2)
        {
            BigInteger remainder;

            while (num2 != 0)
            {
                remainder = num1 % num2;
                num1 = num2;
                num2 = remainder;
            }

            return num1;
        }

        public BigInteger GetE() => Program.ChooseE() == "1" ?
            Program.GetNumberFromConsole() :
            GetRelativelyPrime(phi);
    }
}
