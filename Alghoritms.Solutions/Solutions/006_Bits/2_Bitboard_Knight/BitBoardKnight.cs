using Alghoritms.Solutions.Common;

namespace Alghoritms.Solutions.Solutions
{
    [SolutionDescription(@"6.Bits\2.Bitboard_Knight")]
    public class BitBoardKnight : ISolution
    {
        public string[] Run(string[] input)
        {
            var position = int.Parse(input[0]);
            (var variants, var bitboard) = KnightWalk(position);
            return new[] { variants.ToString(), bitboard.ToString() };
        }

        const ulong KNIGHT_MASK = 0x284400442800UL;
        const ulong RIGHT_EDGE = 0xC0C0C0C0C0C0C0C0UL;
        const ulong LEFT_EDGE = 0x303030303030303UL;
        const int SHIFT = 28;

        public (uint, ulong) KnightWalk(int position)
        {
            //Get mask of the current position
            ulong mask = 1UL << position;

            bool isRightEdge = (mask & RIGHT_EDGE) > 0;
            bool isLeftEdge = (mask & LEFT_EDGE) > 0;

            //Move the Knight to required position
            mask = position > SHIFT
                ? (KNIGHT_MASK << (position - SHIFT))
                : (KNIGHT_MASK >> (SHIFT - position));

            if  (isRightEdge) mask &= ~LEFT_EDGE;
            else if (isLeftEdge) mask &= ~RIGHT_EDGE;

            //Get number of active bits
            uint variants = 0;
            for (var t = mask; t > 0; variants++) t &= (t - 1);
            return (variants, mask);
        }
    }
}