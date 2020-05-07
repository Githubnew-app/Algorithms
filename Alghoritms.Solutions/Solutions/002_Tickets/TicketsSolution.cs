using Alghoritms.Solutions.Common;
using System;
using System.Numerics;

namespace Alghoritms.Solutions
{
    [SolutionDescription("1.Tickets")]
    public class TicketsSolution : ISolution
    {
        public string[] Run(string[] input) => new[] { FildTickets(Int32.Parse(input[0])).ToString() };

        public BigInteger FildTickets(int numberOfDigits)
        {
            long maxSize = 9 * numberOfDigits;
            var values = new long[maxSize + 1];
            for (int i = 0; i <= 9; i++) values[i] = 1;
            for (int n = 1; n < numberOfDigits; n++)
            {
                long localSum = 0;
                long nextSize = 9 * (n + 1);
                long size = nextSize / 2;
                long lastValue = 0;
                for (int i = 0; i <= size; i++)
                {
                    localSum += values[i] - lastValue;
                    values[i] = localSum;
                    lastValue = values[nextSize - i];
                    values[nextSize - i] = localSum;
                }
            }

            BigInteger sum = 0;
            for (int i = 0; i <= maxSize; i++)
            {
                BigInteger value = (BigInteger)values[i];
                sum += value * value;
            }
            return sum;
        }
    }
}
