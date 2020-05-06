using Alghoritms.Solutions.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alghoritms.Solutions
{
    [SolutionDescription("0a.Incantations")]
    public class Incantations : ISolution
    {
        public string[] Run(string[] input) => Incantation(Int32.Parse(input[0]));

        private const int SIZE = 25;
        private Dictionary<int, Func<int, int, bool>> KnownIncantations = new Dictionary<int, Func<int, int, bool>>
        {
            [00] = (x, y) => x > y,
            [01] = (x, y) => x == y,
            [02] = (x, y) => x == SIZE - y - 1,
            [03] = (x, y) => x <= SIZE - y + 4,
            [04] = (x, y) => y == x / 2,
            [05] = (x, y) => x < 10 || y < 10,
            [06] = (x, y) => x > 15 & y > 15,
            [07] = (x, y) => x * y == 0,
            [08] = (x, y) => Math.Abs(x - y) > 10,
            [09] = (x, y) => x > y && (x - 2) < 2 * y,
            [10] = (x, y) => x % (SIZE - 3) == 1 || y % (SIZE - 3) == 1,
            [11] = (x, y) => (x * x + y * y) <= 400,
            [12] = (x, y) => (x + y) >= (SIZE - 5) && (x + y) < (SIZE + 4),
            [13] = (x, y) => ((x - 23) * (x - 23) + (y - 23) * (y - 23)) > 330,
            [14] = (x, y) => (39 - x + y) % 30 > 18,
            [15] = (x, y) => Math.Abs(Math.Abs(x - 12) + Math.Abs(y - 12)) < 10,
            [16] = (x, y) => Math.Sin(x / 3.0) * 8 <= (y - 16),
            [17] = (x, y) => !(x == 0 && y == 0) && (x < 2 || y < 2),
            [18] = (x, y) => (y % (SIZE - 1) == 0) || (x % (SIZE - 1) == 0),
            [19] = (x, y) => (x ^ y) % 2 == 0,
            [20] = (x, y) => x % (y + 1) == 0,
            [21] = (x, y) => (x + y) % 3 == 0,
            [22] = (x, y) => y % 3 == 0 && x % 3 == 0,
            [23] = (x, y) => x == y || ((SIZE - 1) - x == y),
            [24] = (x, y) => y % 6 == 0 || x % 6 == 0,
        };

        public String[] Incantation(int incantationNumber)
        {
            StringBuilder buffer = new StringBuilder();
            var incantation = KnownIncantations[incantationNumber];
            for (int y = 0; y < SIZE; y++)
            {
                if (y > 0) buffer.AppendLine();
                for (int x = 0; x < SIZE; x++)
                {
                    buffer.Append(incantation(x, y) ? '#' : '.');
                }
            }
            return buffer.ToString().Split(Environment.NewLine);
        }
    }
}
