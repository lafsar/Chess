using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Domain
{
	public interface IMoveStrategy
	{
		/// <summary>
		/// Gets possible moves
		/// </summary>
		/// <returns>
		/// Iterable collection of possible moves (row, col) relative from the current piece position
		/// </returns>
		IEnumerable<Tuple<int, int>> GetMoveSet(int row, int col, PieceColor opposingPlayer);
		List<Tuple<int, int>> GetCapturable();
		List<Tuple<int, int>> GetAllMoves();
	}
}
