using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Domain
{
	public class Pawn : ChessPiece, IMoveable
	{
		public bool HasMovedDouble { get; private set; }
		public int MoveCount { get; private set; }

		public Pawn(PieceColor color)
			: base(color)
		{
		}

		public override List<Tuple<int, int>> PossibleStartingPositions()
		{
			var startingRowIndex = PieceColor == PieceColor.Black
				? 1
				: ChessConstants.MAX_BOARD_ROWS - 2;
			return base.PossibleStartingPositions().Where(t => t.Item1 == startingRowIndex).ToList();
		}

		//public void Move(int row, int column)
		//{
		//	var destination = new Tuple<int, int>(row, column);
		//	if (GetMoveSet().Any(t => t.Equals(destination)))
		//	{
		//		HasMovedDouble = MoveCount == 0 && destination.Equals(DoubleForward);
		//		ChessBoardManager.RemoveFromBoard(Row, Column);
		//		Row = row;
		//		Column = column;
		//		HandleCapture(destination);
		//		ChessBoardManager.AddToBoard(this, destination.Item1, destination.Item2);
		//		MoveCount++;
		//	}
		//}

		protected override void HandleCapture(Tuple<int, int> destination)
		{
			if (destination.Equals(DiagonalLeft) || destination.Equals(DiagonalRight))
			{
				if (ChessBoardManager.Board[destination.Item1, destination.Item2] != null)
				{
					ChessBoardManager.RemoveFromBoard(destination.Item1, destination.Item2);
				}
				else
				{
					//For en-passant
					ChessBoardManager.RemoveFromBoard(destination.Item1 + RowDirection, destination.Item2);
				}
			}
		}

		public IEnumerable<Tuple<int, int>> GetMoveSet()
		{
			var currentValidMoves = new List<Tuple<int, int>>() { };
			var defaultForward = new Tuple<int, int>((1 * RowDirection) + Row, Column);

			var isNextRowOccupied = ChessBoardManager.Board[defaultForward.Item1, Column] != null;
			var isNextNextRowOccupied = ChessBoardManager.Board[DoubleForward.Item1, Column] != null;
			var isDiagRightCapturable = !IsRightEdge && ChessBoardManager.Board[DiagonalRight.Item1, DiagonalRight.Item2] != null
				&& ChessBoardManager.Board[DiagonalRight.Item1, DiagonalRight.Item2].PieceColor == OpposingColor;
			var isDiagLeftCapturable = !IsLeftEdge && ChessBoardManager.Board[DiagonalLeft.Item1, DiagonalLeft.Item2] != null
				&& ChessBoardManager.Board[DiagonalLeft.Item1, DiagonalLeft.Item2].PieceColor == OpposingColor;

			var leftAdjacent = !IsLeftEdge ? ChessBoardManager.Board[Row, Column - 1] : null;
			var isDiagLeftPassantable = DeterminePassantable(DiagonalLeft, leftAdjacent);

			var rightAdjacent = !IsRightEdge ? ChessBoardManager.Board[Row, Column + 1] : null;
			var isDiagRightPassantable = DeterminePassantable(DiagonalRight, rightAdjacent);
			//Can move either one step forward, or two steps forward
			//Can only move diagonally to capture a piece if an opposing color is located in either spot diagonally in front
			//Can only move diagonally to an empty spot if en-passant 
			if (!isNextRowOccupied)
			{
				currentValidMoves.Add(defaultForward);
			}
			if (MoveCount == 0 && !isNextRowOccupied && !isNextNextRowOccupied)
			{
				currentValidMoves.Add(DoubleForward);
			}
			if (isDiagRightCapturable || isDiagRightPassantable)
			{
				currentValidMoves.Add(DiagonalRight);
			}
			if (isDiagLeftCapturable || isDiagLeftPassantable)
			{
				currentValidMoves.Add(DiagonalLeft);
			}

			return currentValidMoves;
		}

		private int RowDirection
		{
			get
			{
				return PieceColor == PieceColor.Black
					? 1
					: -1;
			}
		}

		private Tuple<int, int> DoubleForward
		{
			get
			{
				return new Tuple<int, int>((2 * RowDirection) + Row, Column);
			}
		}

		private Tuple<int, int> DiagonalLeft
		{
			get
			{
				return new Tuple<int, int>((1 * RowDirection) + Row, Column - 1);
			}
		}

		private Tuple<int, int> DiagonalRight
		{
			get
			{
				return new Tuple<int, int>((1 * RowDirection) + Row, Column + 1);
			}
		}

		private bool DeterminePassantable(Tuple<int, int> attackDirection, ChessPiece AdjacentEnemy)
		{
			return AdjacentEnemy != null && ChessBoardManager.Board[attackDirection.Item1, attackDirection.Item2] == null
				&& AdjacentEnemy.GetType() == this.GetType() && AdjacentEnemy.PieceColor == OpposingColor && (AdjacentEnemy as Pawn).MoveCount == 1
				&& (AdjacentEnemy as Pawn).HasMovedDouble;
		}
	}
}
