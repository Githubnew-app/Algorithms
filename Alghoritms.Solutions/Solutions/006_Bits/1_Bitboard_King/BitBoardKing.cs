using Alghoritms.Solutions.Common;

namespace Alghoritms.Solutions.Solutions
{
    [SolutionDescription(@"6.Bits\1.Bitboard_King")]
    public class BitBoardKing : ISolution
    {
        public string[] Run(string[] input)
        {
            var position = int.Parse(input[0]);
            (var variants, var bitboard) = KingWalk(position);
            return new[] { variants.ToString(), bitboard.ToString() };
        }

        const ulong RIGHT_EDGE = 0x8080808080808080;
        const ulong LEFT_EDGE = 0x101010101010101;
        const ulong KING_FULL_MASK = 0x3828380000UL;
        const ulong KING_LEFT_MASK = 0x3020300000UL;
        const ulong KING_RIGHT_MASK = 0x1808180000UL;
        const int shift = 28;
        public (uint, ulong) KingWalk(int position)
        {
            //Get mask of the current position
            ulong mask = 1UL << position;

            //Get appropriate mask of the King
            mask = (mask & LEFT_EDGE) > 0
                ? KING_LEFT_MASK
                : (mask & RIGHT_EDGE) > 0
                    ? KING_RIGHT_MASK 
                    : KING_FULL_MASK;

            //Move the King to required position
            mask = position > shift
                ? (mask << (position - shift))
                : (mask >> (shift - position));

            //Get number of active bits
            uint variants = 0;
            for (var t = mask; t > 0; variants++) t &= (t - 1);
            return (variants, mask);
        }
    }
}