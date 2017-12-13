using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Domain
{
	public abstract class Player
	{
		public string Name { get; set; }
		public PieceColor PieceColor { get; set; }
		public bool IsCurrentTurn { get; set; }
		protected IPlayerMediator _mediator;
		public Player(IPlayerMediator mediator)
		{
			IsCurrentTurn = PieceColor == PieceColor.White;
			_mediator = mediator;
		}

		public void MovePiece(Tuple<int, int> from, Tuple<int, int> to)
		{
			var piece = ChessBoard.GetPiece(from.Item1, from.Item2);
			if (IsCurrentTurn && piece != null && piece.PieceColor == PieceColor)
			{
				if (piece.Move(to.Item1, to.Item2))
				{
					AfterMove();
				}
			}
		}

		protected abstract void AfterMove();

		public void PromotePawn(Tuple<int, int> from, string type)
		{
			if (ChessBoard.PromotePawn(from.Item1, from.Item2, PieceColor, type))
			{
				AfterMove();
			}
		}
	}
}
