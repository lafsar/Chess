using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Domain
{
	public interface IMoveable
	{
		/// <summary>
		/// move the current piece to the location according to GetMoveSet
		/// </summary>
		/// <param name="row"></param>
		/// <param name="column"></param>
		void Move(int row, int column);
		/// <summary>
		/// Checks for possible moves 
		/// </summary>
		/// <returns>
		/// Iterable collection of possible moves (row, col) relative from the current piece position
		/// </returns>
		IEnumerable<Tuple<int, int>> GetMoveSet();
	}
}
