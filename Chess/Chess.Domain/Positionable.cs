using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Domain
{
	/// <summary>
	/// Only for managing the location info, not the act of moving
	/// All ChessPieces are positionable
	/// </summary>
	public class Positionable
	{
		private int _row;
		private int _column;
		private int _previous_row;
		private int _previous_column;
		public int Row
		{
			get { return _row; }
			private set {
				_previous_row = _row;
				_row = value;
			}
		}

		public int Column
		{
			get { return _column; }
			private set {
				_previous_column = _column;
				_column = value;
			}
		}

		public int PreviousRow
		{
			get { return _previous_row; }
		}

		public int PreviousColumn
		{
			get { return _previous_column; }
		}

		protected string CurrentPositionAsString()
		{
			var col = ChessConstants.GetColNumToLetter().ContainsKey(Column)
				? ChessConstants.GetColNumToLetter()[Column]
				: null;
			var row = ChessConstants.GetRowNumToLetter().ContainsKey(Row)
				? ChessConstants.GetRowNumToLetter()[Row]
				: null;

			if (row == null || col == null)
			{
				return null;
			}

			return string.Format("{0}{1}", col, row);
		}

		public void SetPosition(int row, int col)
		{
			Row = row;
			Column = col;
		}
	}
}
