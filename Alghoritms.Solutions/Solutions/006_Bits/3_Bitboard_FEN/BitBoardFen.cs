using Alghoritms.Solutions.Common;
using System;
using System.Linq;

namespace Alghoritms.Solutions.Solutions
{
    [SolutionDescription(@"6.Bits\3.Bitboard_FEN")]
    public class BitBoardFen : ISolution
    {
        public string[] Run(string[] input)
        {
            var fen = input[0];
            return FenToBitboard(fen).Select(v => v.ToString()).ToArray();
        }

        static readonly char[] Figures = new[] { 'P', 'N', 'B', 'R', 'Q', 'K', 'p', 'n', 'b', 'r', 'q', 'k' };

        public ulong[] FenToBitboard(String fen)
        {
            ulong[] bitboard = new ulong[12];
            int index = 0, row = 0, pos = 7, len = fen.Length;
            do
            {
                char symb = fen[index];
                switch (symb)
                {
                    case '/': /*New row*/
                        row += 8;
                        pos = 7;
                        break;
                    case char s when s < 57: /*Digit*/
                        pos -= symb - 48;
                        break;
                    default:
                        bitboard[Array.IndexOf(Figures, symb)] |= 0x8000000000000000UL >> (row + pos--);
                        break;
                }
            } while (++index < len);
            return bitboard;
        }
    }
}