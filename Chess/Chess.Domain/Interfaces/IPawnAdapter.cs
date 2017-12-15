using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Domain
{
	/// <summary>
	/// The Pawn is unique to all other ChessPieces is that it cant capture
	/// with its standard moveset (moving forward). It can only capture diagonally.
	/// </summary>
	public interface IPawnAdapter
	{
		IEnumerable<Tuple<int, int>> PawnAdvanceMoves();
		IEnumerable<Tuple<int, int>> PawnCaptureMoves();
	}
}
