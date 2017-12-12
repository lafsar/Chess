using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Domain.Pieces
{
	public sealed class King : ChessPiece
	{
		public King(PieceColor color) : base(color) { }
		public override IEnumerable<Tuple<int, int>> PossibleStartingPositions()
		{
			if (PieceColor == PieceColor.Black)
			{
				yield return new Tuple<int, int>(0, 4);
			}
			else
			{
				yield return new Tuple<int, int>(ChessConstants.MAX_BOARD_ROWS - 1, 4);
			}
		}
	}
}
