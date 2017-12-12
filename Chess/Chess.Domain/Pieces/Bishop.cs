using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Domain.Pieces
{
	public sealed class Bishop : ChessPiece
	{
		public Bishop(PieceColor color) : base(color)
		{
			MoveStrategy = new DiagonalMoveStrategy();
		}
		public override IEnumerable<Tuple<int, int>> PossibleStartingPositions()
		{
			if (PieceColor == PieceColor.Black)
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
