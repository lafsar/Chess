using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Domain
{
	public sealed class Queen : ChessPiece
	{
		public Queen(PieceColor color) : base(color)
		{
			MoveStrategy = new QueenMoveStrategy();
		}
		public static IEnumerable<Tuple<int, int>> PossibleStartingPositions(PieceColor color)
		{
			if (color == PieceColor.Black)
			{
				yield return new Tuple<int, int>(0, 3);
			}
			else
			{
				yield return new Tuple<int, int>(ChessConstants.MAX_BOARD_ROWS - 1, 3);
			}
		}
	}
}
