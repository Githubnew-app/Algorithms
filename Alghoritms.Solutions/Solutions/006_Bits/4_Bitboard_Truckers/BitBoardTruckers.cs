using Alghoritms.Solutions.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alghoritms.Solutions.Solutions
{
    [SolutionDescription(@"6.Bits\4.Bitboard_Truckers")]
    public class BitBoardTruckers : ISolution
    {
        public string[] Run(string[] input)
        {
            var fen = input[0];
            return GetAvailableMoves(fen).Select(v => v.ToString()).ToArray();
        }

        #region Presets

        private enum Pieces : byte
        {
            WhitePawns = (byte)'P',
            WhiteKnights = (byte)'N',
            WhiteBishops = (byte)'B',
            WhiteRooks = (byte)'R',
            WhiteQueens = (byte)'Q',
            WhiteKing = (byte)'K',
            BlackPawns = (byte)'p',
            BlackKnights = (byte)'n',
            BlackBishops = (byte)'b',
            BlackRooks = (byte)'r',
            BlackQueens = (byte)'q',
            BlackKing = (byte)'k',
        }

        static readonly Pieces[] WhitePieces = new Pieces[] {
            Pieces.WhitePawns,
            Pieces.WhiteKnights,
            Pieces.WhiteBishops,
            Pieces.WhiteRooks,
            Pieces.WhiteQueens,
            Pieces.WhiteKing
        };

        static readonly Pieces[] BlackPieces = new Pieces[] {
            Pieces.BlackPawns,
            Pieces.BlackKnights,
            Pieces.BlackBishops,
            Pieces.BlackRooks,
            Pieces.BlackQueens,
            Pieces.BlackKing
        };

        static readonly char[] BitboardLayers = new[] {
            (char)Pieces.WhitePawns,
            (char)Pieces.WhiteKnights,
            (char)Pieces.WhiteBishops,
            (char)Pieces.WhiteRooks,
            (char)Pieces.WhiteQueens,
            (char)Pieces.WhiteKing,
            (char)Pieces.BlackPawns,
            (char)Pieces.BlackKnights,
            (char)Pieces.BlackBishops,
            (char)Pieces.BlackRooks,
            (char)Pieces.BlackQueens,
            (char)Pieces.BlackKing
         };

        enum Edges : ulong
        {
            Left = 0x101010101010101UL,
            Right = 0x8080808080808080UL,
            Top = 0xFF00000000000000UL,
            Bottom = 0xFFUL,
        }

        private enum Directions
        {
            Up = 8,
            Down = -8,
            Left = -1,
            Right = 1,
            UpAndLeft = Up + Left,
            UpAndRight = Up + Right,
            DownAndLeft = Down + Left,
            DownAndRight = Down + Right
        }

        readonly Dictionary<Directions, ulong> LimiterMasks = new Dictionary<Directions, ulong>
        {
            [Directions.Up] = (ulong)Edges.Bottom,
            [Directions.Down] = (ulong)Edges.Top,
            [Directions.Left] = (ulong)Edges.Right,
            [Directions.Right] = (ulong)Edges.Left,
            [Directions.UpAndLeft] = (ulong)Edges.Bottom | (ulong)Edges.Right,
            [Directions.UpAndRight] = (ulong)Edges.Bottom | (ulong)Edges.Left,
            [Directions.DownAndLeft] = (ulong)Edges.Top | (ulong)Edges.Right,
            [Directions.DownAndRight] = (ulong)Edges.Top | (ulong)Edges.Left
        };

        readonly Dictionary<Pieces, Directions[]> Tactics = new Dictionary<Pieces, Directions[]>
        {
            [Pieces.WhiteRooks] = new[] { Directions.Up, Directions.Down, Directions.Left, Directions.Right },
            [Pieces.WhiteBishops] = new[] { Directions.UpAndLeft, Directions.UpAndRight, Directions.DownAndLeft, Directions.DownAndRight },
            [Pieces.WhiteQueens] = new[] { Directions.Up, Directions.Down, Directions.Left, Directions.Right, Directions.UpAndLeft, Directions.UpAndRight, Directions.DownAndLeft, Directions.DownAndRight },
        };

        #endregion

        private ulong[] FenToBitboard(String fen)
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
                        bitboard[Array.IndexOf(BitboardLayers, symbol)] |= 0x8000000000000000UL >> (row + position--);
                        break;
                }
            } while (++index < length);
            return bitboard;
        }

        private ulong BitboardOfPice(ref ulong[] bitboard, Pieces piece) => bitboard[Array.IndexOf(BitboardLayers, (char)piece)];

        private ulong BitboardOfPices(ref ulong[] bitboard, Pieces[] peices)
        {
            ulong board = 0;
            foreach (var figure in peices)
            {
                board |= bitboard[Array.IndexOf(BitboardLayers, (char)figure)];
            }
            return board;
        }

        public ulong[] GetAvailableMoves(String fen)
        {
            ulong[] bitboard = FenToBitboard(fen);
            ulong ownPieces = BitboardOfPices(ref bitboard, WhitePieces);
            ulong oponentsPieces = BitboardOfPices(ref bitboard, BlackPieces);

            ulong rook = AvailabaleMovements(Pieces.WhiteRooks, ref bitboard, ownPieces, oponentsPieces);
            ulong bishop = AvailabaleMovements(Pieces.WhiteBishops, ref bitboard, ownPieces, oponentsPieces);
            ulong queen = AvailabaleMovements(Pieces.WhiteQueens, ref bitboard, ownPieces, oponentsPieces);
            return new[] { rook, bishop, queen };
        }

        private ulong AvailabaleMovements(Pieces figure, ref ulong[] bitboard, ulong ownFigures, ulong oponentsFigures)
        {
            ulong position = BitboardOfPice(ref bitboard, figure);

            ulong movmentMask = 0;
            Directions[] tactics = Tactics[figure];
            foreach (var direction in tactics)
            {
                movmentMask |= AvailabaleMovementsByDirection(position, direction, ownFigures, oponentsFigures);
            }
            return movmentMask;
        }

        private ulong AvailabaleMovementsByDirection(ulong position, Directions direction, ulong ownFigures, ulong oponentsFigures)
        {
            int movment = (int)direction;
            ulong limiterMask = LimiterMasks[direction];
            ulong movmentMask = 0;
            ulong commonLimiterMask = ownFigures | oponentsFigures | limiterMask;
            ulong currentState = movment > 0
                       ? position << movment
                       : position >> -movment;
            while (currentState > 0 && (currentState & commonLimiterMask) == 0)
            {
                movmentMask |= currentState;
                currentState = movment > 0
                       ? currentState << movment
                       : currentState >> -movment;
            }
            if ((currentState & oponentsFigures) > 0
                    && (currentState & limiterMask) == 0)
                movmentMask |= currentState;
            return movmentMask;
        }
    }
}