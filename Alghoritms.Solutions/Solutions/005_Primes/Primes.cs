using Alghoritms.Solutions.Common;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Alghoritms.Solutions.Solutions._005_Primes
{
    /*
На первой строчке записано целое число N >= 1. 
Найти количество простых чисел от 1 до N.

Решить задачу разными способами.
1. Через перебор делителей.
2abcdef. Несколько оптимизаций перебора делителей
3. Решето Эратосфена со сложностью O(n log log n).
4. Решето Эратосфена с оптимизацией памяти: битовая матрица, по 32 значения в одном int
5. Решето Эратосфена со сложностью O(n)
     */
    [SolutionDescription(@"5.Primes", actual: true)]
    public class Primes : ISolution
    {
        public string[] Run(string[] input)
        {
            var number = ulong.Parse(input[0]);
            return new[] { TrialDivision_4(number).ToString() };
        }

        public ulong TrialDivision_0(ulong n)
        {
            ulong result = 0;
            for (ulong i = 2; i <= n; i++)
            {
                if (IsPrime(i)) result++;
            }
            return result;

            static bool IsPrime(ulong n)
            {
                for (ulong i = 2; i < n; i++)
                {
                    if (n % i == 0)
                        return false;
                }
                return true;
            }
        }

        public ulong TrialDivision_1(ulong n)
        {
            ulong result = 0;
            for (ulong i = 2; i <= n; i++)
            {
                if (IsPrime(i))
                    result++;
            }
            return result;

            static bool IsPrime(ulong n)
            {
                if (n <= 3) return n > 1;
                if (n % 2 == 0) return false;
                for (ulong i = 3; i < n; i += 2)
                {
                    if (n % i == 0)
                        return false;
                }
                return true;
            }
        }

        public ulong TrialDivision_2(ulong n)
        {
            ulong result = 0;
            for (ulong i = 2; i <= n; i++)
            {
                if (IsPrime(i))
                    result++;
            }
            return result;

            static bool IsPrime(ulong n)
            {
                if (n <= 3) return n > 1;
                if (n % 2 == 0) return false;
                if (n % 3 == 0) return false;
                ulong k = 1;
                ulong t;
                do
                {
                    t = 6 * k - 1;
                    if (t < n && n % t == 0) return false;
                    t += 2;
                    if (t < n && n % t == 0) return false;
                    k++;
                } while (t < n);
                return true;
            }
        }

        public ulong TrialDivision_3(ulong n)
        {
            ulong result = 0;
            for (ulong i = 2; i <= n; i++)
            {
                if (IsPrime(i))
                    result++;
            }
            return result;

            static bool IsPrime(ulong n)
            {
                if (n <= 3) return n > 1;
                if (n % 2 == 0) return false;
                if (n % 3 == 0) return false;
                ulong k = 1;
                ulong t;
                ulong max = (ulong)Math.Sqrt(n);
                do
                {
                    t = 6 * k - 1;
                    if (t < n && n % t == 0) return false;
                    t += 2;
                    if (t < n && n % t == 0) return false;
                    k++;
                } while (t <= max);
                return true;
            }
        }

        public ulong TrialDivision_4(ulong n)
        {
            if (n < 5) return n > 3 ? 2 : n - 1;
            ulong t, k = 1, max = n, result = 2;
            while(true)
            {
                if ((t = 6 * k - 1) > max) break;
                if (IsPrime(t)) result++;
                if ((t += 2) > max) break;
                if (IsPrime(t)) result++;
                k++;
            }
            return result;

            static bool IsPrime(ulong n)
            {
                ulong t, k = 1, max = (ulong)Math.Sqrt(n);
                while(true)
                {
                    if ((t = 6 * k - 1) > max)
                        return true;
                    if (n % t == 0)
                        return false;
                    if ((t += 2) > max)
                        return true;
                    if (n % t == 0) 
                        return false;
                    k++;
                }
            }
        }
    }
}
