using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Domain
{
	public static class ChessConstants
	{
		public const int MAX_BOARD_ROWS = 8;
		public const int MAX_BOARD_COLUMNS = 8;

		public static Dictionary<int, string> GetColNumToLetter()
		{
			return new Dictionary<int, string> {
				{ 0, "a" },
				{ 1, "b" },
				{ 2, "c" },
				{ 3, "d" },
				{ 4, "e" },
				{ 5, "f" },
				{ 6, "g" },
				{ 7, "h" }
			};
		}

		public static Dictionary<int, string> GetRowNumToLetter()
		{
			return new Dictionary<int, string> {
				{ 0, "8" },
				{ 1, "7" },
				{ 2, "6" },
				{ 3, "5" },
				{ 4, "4" },
				{ 5, "3" },
				{ 6, "2" },
				{ 7, "1" }
			};
		}
	}
}
