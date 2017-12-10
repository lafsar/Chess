using System;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Domain
{
	/// <summary>
	/// All new chess pieces should derive from this class
	/// </summary>
    public abstract class ChessPiece<T> : Positionable where T : ChessPiece<T>
    {
		protected IMoveable MoveStrategy { get; set; }
		public abstract T GetInstance();
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

		public virtual void Move(int row, int column)
		{
			var destination = new Tuple<int, int>(row, column);
			if (MoveStrategy.GetMoveSet().Any(t => t.Equals(destination)))
			{
				//HasMovedDouble = MoveCount == 0 && destination.Equals(DoubleForward);
				ChessBoardManager.RemoveFromBoard(Row, Column);
				Row = row;
				Column = column;
				HandleCapture(destination);
				ChessBoardManager.AddToBoard(GetInstance(), destination.Item1, destination.Item2);
				//MoveCount++;
			}
		}

		public ChessPiece(PieceColor pieceColor)
        {
			PieceColor = pieceColor;
		}

        public override string ToString()
        {
            return CurrentPositionAsString();
        }

        protected string CurrentPositionAsString()
        {
            return string.Format("Current Row:{1}{0} Current Column:{2}{0} Piece Color: {1}", Environment.NewLine, Row, Column, PieceColor);
        }

		protected abstract void HandleCapture(Tuple<int, int> destination);
	}
}
