using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Domain.Pieces
{
	public sealed class Queen : ChessPiece
	{
		public Queen(PieceColor color) : base(color)
		{
			MoveStrategy = new QueenMoveStrategy();
		}
		public override IEnumerable<Tuple<int, int>> PossibleStartingPositions()
		{
			if (PieceColor == PieceColor.Black)
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
