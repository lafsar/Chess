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
			protected set {
				_previous_row = _row;
				_row = value;
			}
		}

		public int Column
		{
			get { return _column; }
			protected set {
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

		public string CurrentChessPosition()
		{
			var col = ChessConstants.GetColNumToLetter().ContainsKey(Column)
				? ChessConstants.GetColNumToLetter()[Column]
				: null;
			var row = ChessConstants.GetRowNumToLetter().ContainsKey(Row)
				? ChessConstants.GetRowNumToLetter()[Row]
				: null;

			if (row == null || col == null)
			{
				return "Not a valid position!";
			}

			return string.Format("{0}{1}", col, row);
		}
		/// <summary>
		/// Absolute starting positions on a full board
		/// This is the default implementation, you should override this in specific pieces
		/// </summary>
		/// <returns>
		/// Iterable collection of row, col
		/// </returns>
		public virtual List<Tuple<int, int>> PossibleStartingPositions()
		{
			var blackPieces = new List<Tuple<int, int>> {};

			var whitePieces = new List<Tuple<int, int>> {};

			for (var r = 0; r < 2; r++)
			{
				for (var c = 0; c < ChessConstants.MAX_BOARD_COLUMNS; c++)
				{
					blackPieces.Add(new Tuple<int, int>(r, c));
					whitePieces.Add(new Tuple<int, int>(r + 6, c));
				}
			}
			blackPieces.AddRange(whitePieces);
			return blackPieces;
		}

		public void SetInitialPosition(int row, int col)
		{
			if (PossibleStartingPositions().Any(t => t.Item1 == row && t.Item2 == col)) { 
				Row = row;
				Column = col;
			} else
			{
				throw new Exception("Not a valid starting position for this piece!");
			}
		}

		public void SetPosition(int row, int col)
		{
			Row = row;
			Column = col;
		}

		protected bool IsTopLeftCorner
		{
			get
			{
				return IsTopEdge && IsLeftEdge;
			}
		}

		protected bool IsTopRightCorner
		{
			get
			{
				return IsTopEdge && IsRightEdge;
			}
		}

		protected bool IsBottomRightCorner
		{
			get
			{
				return IsBottomEdge && IsRightEdge;
			}
		}

		protected bool IsBottomLeftCorner
		{
			get
			{
				return IsBottomEdge && IsLeftEdge;
			}
		}

		protected bool IsTopEdge
		{
			get
			{
				return Row == 0;
			}
		}

		protected bool IsBottomEdge
		{
			get
			{
				return Row == ChessConstants.MAX_BOARD_ROWS - 1;
			}
		}

		protected bool IsLeftEdge
		{
			get
			{
				return Column == 0;
			}
		}

		protected bool IsRightEdge
		{
			get
			{
				return Column == ChessConstants.MAX_BOARD_COLUMNS - 1;
			}
		}
	}
}
