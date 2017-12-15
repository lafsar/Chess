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
		public BaseMoveStrategy MoveStrategy { get; set; }
        private PieceColor _pieceColor;
		public int MoveCount { get; set; }
		protected ChessBoard ChessBoard;
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
		//TODO: inject optional Player instance to keep track of the current player score
		protected ChessPiece(PieceColor pieceColor, ChessBoard board)
		{
			PieceColor = pieceColor;
			ChessBoard = board;
		}

		public virtual bool Move(int row, int column)
		{
			var destination = new Tuple<int, int>(row, column);
			var origin = new Tuple<int, int>(Row, Column);
			var canMove = !destination.Equals(origin) 
				&& MoveStrategy.GetMoveSet(Row, Column, OpposingColor).Any(t => t.Equals(destination));
			
			if (canMove && !ChessBoard.IsCheckedState(this, destination))
			{
				BeforeMove(destination);
				ChessBoard.Remove(Row, Column);
				HandleCapture(destination);
				MoveCount++;
				AfterMove();
				ChessBoard.AddReplace(this, destination.Item1, destination.Item2);
			}
			return canMove;
		}

		public virtual void BeforeMove(Tuple<int, int> destination) {}
		public virtual void AfterMove() {
			
		}

		public abstract void Accept(IChessPieceVisitor visitor);

        public override string ToString()
        {
            return CurrentPositionAsString();
        }

		public virtual void HandleCapture(Tuple<int, int> destination)
		{
			//Most pieces will auto-replace the enemy piece when moving to an occupied location, but pawns are special.
		}
	}
}
