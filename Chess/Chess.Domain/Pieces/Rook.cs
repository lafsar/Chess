using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Domain
{
	public sealed class Rook : ChessPiece
	{
		public Rook(PieceColor color, ChessBoard board) : base(color, board)
		{
			MoveStrategy = new RookMoveStrategy(board);
		}
		public static IEnumerable<Tuple<int,int>> PossibleStartingPositions(PieceColor color)
		{
			if (color == PieceColor.Black)
			{
				yield return new Tuple<int, int>(0, 0);
				yield return new Tuple<int, int>(0, ChessConstants.MAX_BOARD_COLUMNS - 1);
			}
			else
			{
				yield return new Tuple<int, int>(ChessConstants.MAX_BOARD_ROWS - 1, 0);
				yield return new Tuple<int, int>(ChessConstants.MAX_BOARD_ROWS - 1, ChessConstants.MAX_BOARD_COLUMNS - 1);
			}
		}
	}
}
