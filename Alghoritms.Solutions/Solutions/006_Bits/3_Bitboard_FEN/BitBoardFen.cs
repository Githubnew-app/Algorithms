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

        static readonly char[] Pieces = new[] { 'P', 'N', 'B', 'R', 'Q', 'K', 'p', 'n', 'b', 'r', 'q', 'k' };

        public ulong[] FenToBitboard(String fen)
        {
            ulong[] bitboard = new ulong[12];
            int index = 0, row = 0, position = 7, length = fen.Length;
            do
            {
                char symbol = fen[index];
                switch (symbol)
                {
                    case '/': /*New row*/
                        row += 8;
                        position = 7;
                        break;
                    case char s when s < 57: /*Digit*/
                        position -= symbol - 48;
                        break;
                    default:
                        bitboard[Array.IndexOf(Pieces, symbol)] |= 0x8000000000000000UL >> (row + position--);
                        break;
                }
            } while (++index < length);
            return bitboard;
        }
    }
}