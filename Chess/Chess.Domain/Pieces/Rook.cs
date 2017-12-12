using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Domain.Pieces
{
	public sealed class Rook : ChessPiece
	{
		public Rook(PieceColor color) : base(color)
		{
			MoveStrategy = new RookMoveStrategy();
		}
		public override IEnumerable<Tuple<int,int>> PossibleStartingPositions()
		{
			if (PieceColor == PieceColor.Black)
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
