using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Domain
{
	public sealed class Bishop : ChessPiece
	{
		public Bishop(PieceColor color, ChessBoard board) : base(color, board)
		{
			MoveStrategy = new DiagonalMoveStrategy(board);
		}
		public static IEnumerable<Tuple<int, int>> PossibleStartingPositions(PieceColor color)
		{
			if (color == PieceColor.Black)
			{
				yield return new Tuple<int, int>(0, 2);
				yield return new Tuple<int, int>(0, 5);
			} else
			{
				yield return new Tuple<int, int>(ChessConstants.MAX_BOARD_ROWS - 1, 2);
				yield return new Tuple<int, int>(ChessConstants.MAX_BOARD_ROWS - 1, 5);
			}
		}
	}
}
