using Alghoritms.Solutions.Common;
using System;

namespace Alghoritms.Solutions.Solutions
{
    [SolutionDescription(@"5.Primes")]
    public class Primes : ISolution
    {
        public string[] Run(string[] input)
        {
            var number = ulong.Parse(input[0]);
            return new[] { Erotosfen_7(number).ToString() };
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
    
        public ulong Erotosfen_1(ulong n)
        {
            ulong count = 0;
            ulong[] store = new ulong[n + 1];
            for (ulong i = 2; i <= n; i++)
            {
                if (store[i] == 0)
                {
                    count++;
                    for (ulong j = i * 2; j <= n; j+=i)
                    {
                        store[j] = 1;
                    }
                }
            }
            return count;
        }

        public ulong Erotosfen_2(ulong n)
        {
            ulong count = 0;
            ulong[] store = new ulong[(n >> 6) + 1];
            for (ulong i = 2; i <= n; i++)
            {
                ulong value = 1UL << (int)(i % 64);
                if ((store[i >> 6] & value) == 0UL)
                {
                    count++;
                    for (ulong j = i * 2; j <= n; j += i)
                    {
                        store[j >> 6] |= (1UL << (int)(j % 64));
                    }
                }
            }
            return count;
        }

        public ulong Erotosfen_3(ulong n)
        {
            ulong count = 0;
            ulong[] store = new ulong[n + 1];
            ulong max = (ulong)Math.Sqrt(n);
            for (ulong i = 2; i <= max; i++)
            {
                if (store[i] == 0)
                {
                    for (ulong j = i * 2; j <= n; j += i)
                    {
                        store[j] = 1;
                    }
                }
            }
            for (ulong i = 2; i <= n; i++)
            {
                if(store[i] == 0)
                count++;
            }
            return count;
        }

        public ulong Erotosfen_4(ulong n)
        {
            ulong count = 0;
            ulong[] store = new ulong[n + 1];
            for (ulong m = 4; m <= n; m += 2)
            {
                store[m] = 1;
            }
            ulong j, i = 2;
            while ((j = i * i) <= n)
            {
                ulong k = j;
                while (k <= n)
                {
                    store[k] = 1;
                    k += i;
                }
                i++;
            }
            for (ulong m = 2; m <= n; m++)
            {
                if (store[m] == 0)
                    count++;
            }
            return count;
        }

        public ulong Erotosfen_5(ulong n)
        {
            if (n < 2) return 0;
            ulong[] store = new ulong[n + 1];
            ulong j, i = 3;
            while ((j = i * i) <= n)
            {
                if (store[j] == 0)
                {
                    ulong k = j;
                    while (k <= n)
                    {
                        store[k] = 1;
                        k += i << 1;
                    }
                }
                i += 2;
            }
            ulong count = 1;
            for (ulong m = 3; m <= n; m += 2)
            {
                if (store[m] == 0)
                    count++;
            }
            return count;
        }

        public ulong Erotosfen_6(ulong n)
        {
            if (n < 2) return 0;
            ulong length = (n >> 6) + 1;
            ulong[] store = new ulong[length];
            for (ulong m = 1; m < length; m++)
            {
                store[m] = 0xAAAAAAAAAAAAAAAA;
            }
            store[0] = 0xAAAAAAAAAAAAAAA9;
            ulong i = 3;
            ulong j;
            while ((j = i * i) <= n)
            {
                ulong value = 1UL << ((int)(j % 64) - 1);
                if ((store[j >> 6] & value) == 0)
                {
                    ulong k = j;
                    while (k <= n)
                    {
                        store[k >> 6] |= 1UL << ((int)(k % 64) - 1);
                        k += i << 1;
                    }
                }
                i += 2;
            }
            ulong count = 0;
            ulong bitCount;
            if (length > 1)
            {
                for (ulong m = 0; m < length - 1; m++)
                {
                    bitCount = 0;
                    for (var t = store[m]; t > 0; bitCount++) t &= (t - 1);
                    count += 64 - bitCount;
                }
            }
            int len = (int)n % 64;
            ulong mask = ((1UL << len) - 1);
            bitCount = 0;
            for (var t = (store[length - 1] & mask); t > 0; bitCount++) t &= (t - 1);
            count += (ulong)len - bitCount;
            return count;
        }

        /// <summary>
        /// With optimizations by memory and speed
        /// </summary>
        public ulong Erotosfen_7(ulong n)
        {
            ulong b = 0x3FUL;
            if (n < 2) return 0;
            ulong k, l, m = 3, length = ((n - 1) >> 7) + 1;
            ulong[] store = new ulong[length];
            while ((l = m * m) <= n)
            {
                ulong value = 1UL << (int)((l >> 1) & b);
                if ((store[l >> 7] & value) == 0)
                {
                    k = l;
                    while (k <= n)
                    {
                        value = 1UL << (int)((k >> 1) & b);
                        store[k >> 7] |= value;
                        k += m << 1;
                    }
                }
                m += 2;
            }
            ulong bitCount, count = 1;
            store[0] |= 1;
            if (length > 1)
            {
                for (ulong i = 0; i < length - 1; i++)
                {
                    bitCount = 64;
                    for (var t = store[i]; t > 0; bitCount--) t &= t - 1;
                    count += bitCount;
                }
            }

            int len = (int)(((n - 1) >> 1) & b) + 1;
            ulong mask = (len == 64) ? 0xFFFFFFFFFFFFFFFFUL : ((1UL << len) - 1);
            bitCount = (ulong)len;
            for (var t = store[length - 1] & mask; t > 0; bitCount--) t &= t - 1;
            count += bitCount;
            return count;
        }
    }
}
