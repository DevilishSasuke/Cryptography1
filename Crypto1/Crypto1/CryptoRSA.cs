using System.Numerics;
using System.Security.AccessControl;
using static System.Net.Mime.MediaTypeNames;

namespace Cryptography
{
    // Шифрование алгоритмом RSA
    public class CryptoRSA
    {
        public BigInteger p { get; private set; } = BigInteger.Zero;
        public BigInteger q { get; private set; } = BigInteger.Zero;
        public BigInteger N { get; private set; } = BigInteger.Zero;
        public BigInteger e { get; private set; } = BigInteger.Zero;
        public BigInteger d { get; private set; } = BigInteger.Zero;
        public BigInteger phi { get; private set; } = BigInteger.Zero;
        private readonly PrimeGenerator rand = new PrimeGenerator();

        // Генерация полностью новых ключей
        public void GenerateNewKeys()
        {
            // p и q - взаимно простые, простые случайно сгенерированные числа
            p = rand.GeneratePrime();
            q = rand.GeneratePrime();

            N = p * q;

            phi = EulerFunc(p, q);
            e = GetE(); // Получаем E на выбор
            d = (EuclidExtended(e, phi).Item2 + phi) % phi;
        }

        // Зашифровка текста
        public BigInteger EncryptText(BigInteger text) => ChineseTheoreme(text, e);

        // Расшифровка текста
        // С китайской теоремой об остатках
        public BigInteger DecryptText(BigInteger text) => ChineseTheoreme(text, d);

        public (BigInteger, BigInteger) GetPublicKey() => (e, N);
        public (BigInteger, BigInteger) GetPrivateKey() => (d, N);

        // Возведение в степень по модулю
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

        // Китайская теорема об остатках для RSA
        private BigInteger ChineseTheoreme(BigInteger T, BigInteger atr)
        {
            BigInteger M, M1, M2, atr1, atr2;
            atr1 = atr % (p - 1);
            atr2 = atr % (q - 1);
            M1 = ModExp(T, atr1, p);
            M2 = ModExp(T, atr2, q);
            var x = EuclidExtended(q, p).Item2;
            var r = x % p;
            M = (((M1 - M2) * r) % p) * q + M2;
            return M;
        }

        // Функция Эйлера
        // Количество меньших взаимно-простых чисел
        public BigInteger EulerFunc(BigInteger p, BigInteger q) => (p - 1) * (q - 1);

        // Алгоритм Евклида
        // Нахождение НОД
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

        // Расширенный алгоритм Евклида
        // Нахождение НОД и линейной комбинации
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
        
        // Получить взаимно простое число относительно a
        public BigInteger GetRelativelyPrime(BigInteger a)
        {
            BigInteger b;

            while (true)
            {
                b = rand.GeneratePrime();
                if (b >= a && b <= 2) continue;

                for (b = b % a; b < a; ++b)
                {
                    if (Euclid(a, b) == 1)
                        return b;
                }
            }
        }

        // Получение E на выбор
        // Вводом или сгенерированная
        public BigInteger GetE() => Program.ChooseE() == "1" ?
            Program.GetNumberFromConsole() :
            GetRelativelyPrime(phi);
    }
}
