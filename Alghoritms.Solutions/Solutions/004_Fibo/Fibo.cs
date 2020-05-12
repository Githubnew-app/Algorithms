using Alghoritms.Solutions.Common;
using System;
using System.Numerics;

namespace Alghoritms.Solutions.Solutions
{
    [SolutionDescription(@"4.Fibo")]
    public class Fibo : ISolution
    {
        public string[] Run(string[] input)
        {
            var number = ulong.Parse(input[0]);
            return new[] { Matrix(number).ToString() };
        }

        private BigInteger Recursion(ulong n)
            => (n <= 1) ? n : (Recursion(n - 1) + Recursion(n - 2));

        private BigInteger Iterations(ulong n)
        {
            if (n <= 1) return n;
            BigInteger result = 1, previous = 1, tmp, max = n;
            for (BigInteger i = 2; i < max; i++)
            {
                tmp = result;
                result += previous;
                previous = tmp;
            }
            return result;
        }


        private BigInteger GoldenRatio(ulong n)
        {
            double sqrt5 = Math.Sqrt(5);
            double phi = (1 + sqrt5) / 2.0;
            double res = Math.Pow(phi, n) / sqrt5;
            return (BigInteger)(res + 0.5);
        }

        private BigInteger Matrix(ulong n)
        {
            if (n <= 1) return n;
            BigInteger m0 = 0, m1 = 1, m2 = 1;
            BigInteger r0 = 0, r2 = 1, t2, t1;
            BigInteger r1 = ((n & 1) == 1) ? 1 : 0;
            for (; n > 0;)
            {
                t1 = m1;
                t2 = m2;
                m2 = m2 * m2 + m1 * m1;
                m1 = m1 * t2 + m0 * m1;
                m0 = t1 * t1 + m0 * m0;
                if (((n >>= 1) & 1) == 1)
                {
                    t1 = r1;
                    t2 = r2;
                    r2 = m2 * r2 + m1 * r1;
                    r1 = m1 * t2 + m0 * r1;
                    r0 = m1 * t1 + m0 * r0;
                }
            }
            return r1;
        }
    }
}
