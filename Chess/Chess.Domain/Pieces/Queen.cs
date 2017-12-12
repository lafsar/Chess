using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Domain.Pieces
{
	public sealed class Queen : ChessPiece
	{
		public Queen(PieceColor color) : base(color) { }
		public override IEnumerable<Tuple<int, int>> PossibleStartingPositions()
		{
			return new List<Tuple<int, int>>().AsEnumerable();
		}
	}
}
