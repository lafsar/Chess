using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Domain
{
	/// <summary>
	/// Players must subscribe to a ChessGame in order to start playing
	/// TODO: keep track of score, but I'm not familiar enough with the pointing system in chess
	/// </summary>
	public abstract class Player
	{
		public string Name { get; set; }
		public PieceColor PieceColor { get; set; }
		public bool IsCurrentTurn { get; set; }
		protected IPlayerMediator _mediator;
		public ChessBoard ChessBoard { get; set; }
		public Player(IPlayerMediator mediator)
		{
			IsCurrentTurn = PieceColor == PieceColor.White;
			_mediator = mediator;
		}

		public void MovePiece(Tuple<int, int> from, Tuple<int, int> to)
		{
			var piece = ChessBoard.GetPiece(from.Item1, from.Item2);
			if (_mediator.IsGameStarted() && IsCurrentTurn && piece != null && piece.PieceColor == PieceColor)
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
			if (_mediator.IsGameStarted() && ChessBoard.PromotePawn(from.Item1, from.Item2, PieceColor, type))
			{
				AfterMove();
			}
		}

		public void CastleKing(int direction)
		{
			if (_mediator.IsGameStarted() && ChessBoard.CastleKing(PieceColor, direction))
			{
				AfterMove();
			}
		}
	}
}
