using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Domain.Pieces
{
	public sealed class Knight : ChessPiece
	{
		public Knight(PieceColor color) : base(color) { }
		public override IEnumerable<Tuple<int, int>> PossibleStartingPositions()
		{
			if (PieceColor == PieceColor.Black)
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
