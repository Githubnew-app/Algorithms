using Alghoritms.Solutions.Common;
using System.Numerics;

namespace Alghoritms.Solutions.Solutions
{
    [SolutionDescription(@"2.GCD", actual: true)]
    public class Gcd : ISolution
    {
        public string[] Run(string[] input)
        {
            var number = BigInteger.Parse(input[0]);
            var rank = BigInteger.Parse(input[1]);
            return new[] { BitOperations(number, rank).ToString("0.####################") };
        }

        public BigInteger Decrement(BigInteger a, BigInteger b)
        {
            while(a != b)
            {
                if (a > b) a -= b;
                else b -= a;}
            return a;
        }

        public BigInteger RemainderOfDivision(BigInteger a, BigInteger b)
        {
            BigInteger gcd;
            do
            {
                if (a > b)
                {
                    if ((a %= (gcd = b)) == 0) return gcd;
                }
                else
                {
                    if ((b %= (gcd = a)) == 0) return gcd;
                }
            } while (true);
        }

        public BigInteger BitOperations(BigInteger a, BigInteger b)
        {
            if (a == 0 || b == 0) return 0;
            if (a == 1 || a == b) return a;
            if (b == 1) return b;
            BigInteger multiplayer = 1;
            while (((a | b) & 1) == 0)
            {
                multiplayer <<= 1;
                a >>= 1;
                b >>= 1;
            }
            do
            {
                {
                    while ((a & 1) == 0)
                    {
                        a >>= 1;
                        if (a == 1) break;
                    }
                    while ((b & 1) == 0)
                    {
                        b >>= 1;
                        if (b == 1)
                        {
                            a = 1;
                            break;
                        }
                    }
                    if (a == b) break;
                    if (a > b) a -= b;
                    else b -= a;
                }
            } while (a != b);
            return a * multiplayer;
        }
    }
}
