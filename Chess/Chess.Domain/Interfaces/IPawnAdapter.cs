using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Domain
{
	public interface IPawnAdapter
	{
		IEnumerable<Tuple<int, int>> PawnAdvanceMoves();
		IEnumerable<Tuple<int, int>> PawnCaptureMoves();
	}
}
