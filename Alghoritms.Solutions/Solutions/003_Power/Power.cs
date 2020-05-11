using Alghoritms.Solutions.Common;
using System;

namespace Alghoritms.Solutions.Solutions
{
    [SolutionDescription(@"3.Power")]
    [SolutionDescription(@"3.Power/Additional", actual: true)]
    public class Power : ISolution
    {
        public string[] Run(string[] input)
        {
            var number = decimal.Parse(input[0]);
            var rank = UInt64.Parse(input[1]);
            return new[] { PowerViaBinaryExpansion(number, rank).ToString("0.####################") };
        }

        public decimal PowerViaIterations(decimal number, ulong rank)
        {
            if (rank == 0) return 1;
            if (rank == 1) return number;
            decimal result = number;
            for (ulong i = 1; i < rank; i++) result *= number;
            return result;
        }

        public decimal PowerPowOf2WithAdditionaMultiplying(decimal number, ulong rank)
        {
            if (rank <= 1) return rank == 0 ? 1 : number;
            decimal result = number;
            ulong powOfTwo = 2;
            for (; powOfTwo <= rank; powOfTwo <<= 1) result *= result;
            for (ulong i = powOfTwo >> 1; i < rank; i++) result *= number;
            return result;
        }

        public decimal PowerViaBinaryExpansion(decimal number, ulong rank)
        {
            decimal result = (rank & 1) == 1 ? number : 1;
            for (decimal power = number; rank > 0; )
            {
                power *= power;
                if (((rank >>= 1) & 1) == 1) result *= power;
            }
            return result;
        }
    }
}
