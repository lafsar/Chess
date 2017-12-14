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
		public int MoveCount { get; protected set; }
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
		protected ChessPiece(PieceColor pieceColor, ChessBoard board)
		{
			PieceColor = pieceColor;
			ChessBoard = board;
		}

		public virtual bool Move(int row, int column)
		{
			var destination = new Tuple<int, int>(row, column);
			var origin = new Tuple<int, int>(Row, Column);
			var canMove = MoveStrategy.GetMoveSet(Row, Column, OpposingColor).Any(t => t.Equals(destination));
			//Check to see if next move will put us in check
			ChessBoard.UpdateBoardState(destination);
			var willBeChecked = PieceColor == PieceColor.Black
				? ChessBoard.BlackKing.IsInCheck
				: ChessBoard.WhiteKing.IsInCheck;
			//Reset state back to what it was
			ChessBoard.UpdateBoardState(origin);
			if (canMove && !willBeChecked)
			{
				ChessBoard.RemovePiece(Row, Column);
				SetPosition(row, column);
				HandleCapture(destination);
				MoveCount++;
				ChessBoard.AddOrReplacePiece(this, destination.Item1, destination.Item2);
				AfterMove();
			}
			return canMove;
		}

		protected virtual void BeforeMove() {}
		protected virtual void AfterMove() {
			
		}

        public override string ToString()
        {
            return CurrentPositionAsString();
        }

		protected virtual void HandleCapture(Tuple<int, int> destination)
		{
			//if (ChessBoard.GetPiece(destination.Item1, destination.Item2) != null)
			//{
			//	ChessBoard.RemovePiece(destination.Item1, destination.Item2);
			//}
		}
	}
}
