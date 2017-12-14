using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Domain
{
	public sealed class Knight : ChessPiece
	{
		public Knight(PieceColor color, ChessBoard board) : base(color, board)
		{
			MoveStrategy = new KnightMoveStrategy(board);
		}
		public static IEnumerable<Tuple<int, int>> PossibleStartingPositions(PieceColor color)
		{
			if (color == PieceColor.Black)
			{
				yield return new Tuple<int, int>(0, 1);
				yield return new Tuple<int, int>(0, 6);
			}
			else
			{
				yield return new Tuple<int, int>(ChessConstants.MAX_BOARD_ROWS - 1, 1);
				yield return new Tuple<int, int>(ChessConstants.MAX_BOARD_ROWS - 1, 6);
			}
		}
	}
}
