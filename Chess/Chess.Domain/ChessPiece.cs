using System;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Domain
{
	/// <summary>
	/// All new chess pieces should derive from this class
	/// </summary>
    public abstract class ChessPiece : Positionable
    {
		public IMoveStrategy MoveStrategy { get; set; }
        private PieceColor _pieceColor;

		public PieceColor PieceColor
        {
            get { return _pieceColor; }
            private set { _pieceColor = value; }
        }

		public PieceColor OpposingColor
		{
			get
			{
				return PieceColor == PieceColor.Black
					? PieceColor.White
					: PieceColor.Black;
			}
		}
		protected ChessPiece(PieceColor pieceColor)
		{
			PieceColor = pieceColor;
		}

		public virtual bool Move(int row, int column)
		{
			var destination = new Tuple<int, int>(row, column);
			var canMove = MoveStrategy.GetMoveSet(Row, Column, OpposingColor).Any(t => t.Equals(destination));
			if (canMove)
			{
				BeforeMove();
				ChessBoard.RemovePiece(Row, Column);
				SetPosition(row, column);
				HandleCapture(destination);
				ChessBoard.AddPiece(this, destination.Item1, destination.Item2);
				AfterMove();
			}
			return canMove;
		}

		protected virtual void BeforeMove() { }
		protected virtual void AfterMove() { }

        public override string ToString()
        {
            return CurrentPositionAsString();
        }

		protected virtual void HandleCapture(Tuple<int, int> destination)
		{
			if (ChessBoard.GetPiece(destination.Item1, destination.Item2) != null)
			{
				ChessBoard.RemovePiece(destination.Item1, destination.Item2);
			}
		}

		/// <summary>
		/// Absolute starting positions on a full board
		/// </summary>
		/// <returns>
		/// Iterable collection of row, col
		/// </returns>
		public abstract IEnumerable<Tuple<int, int>> PossibleStartingPositions();

		public void SetInitialPosition(int row, int col)
		{
			if (PossibleStartingPositions().Any(t => t.Item1 == row && t.Item2 == col))
			{
				SetPosition(row, col);
			}
			else
			{
				throw new Exception("Not a valid starting position for this piece!");
			}
		}
	}
}
