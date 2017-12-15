using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Domain
{
	/// <summary>
	/// Pawns move in specific directions
	/// Black moves from top to bottom
	/// White moves from bottom to top
	/// </summary>
	public sealed class Pawn : ChessPiece
	{
		public bool HasMovedDouble { get; private set; }
		public int Direction
		{
			get
			{
				return PieceColor == PieceColor.Black
					? 1
					: -1;
			}
		}

		public bool IsPromoteAble
		{
			get
			{
				return (Direction == 1 && Row == ChessConstants.MAX_BOARD_ROWS - 1)
					|| (Direction == -1 && Row == 0);
			}
		}
		
		public Pawn(PieceColor color, ChessBoard board)
			: base(color, board)
		{
			MoveStrategy = new PawnAdapterStrategy(MoveCount, Direction, board);
		}

		public static IEnumerable<Tuple<int, int>> PossibleStartingPositions(PieceColor color)
		{
			for (var c = 0; c < ChessConstants.MAX_BOARD_COLUMNS; c++)
			{
				if (color == PieceColor.Black)
				{
					yield return new Tuple<int, int>(1, c);
				} else
				{
					yield return new Tuple<int, int>(ChessConstants.MAX_BOARD_ROWS - 2, c);
				}
			}
		}
		public override void BeforeMove(Tuple<int, int> destination)
		{
			HasMovedDouble = MoveCount == 0 && destination.Equals((MoveStrategy as PawnAdapterStrategy).DoubleForward);
		}

		public override void AfterMove()
		{
			//This is to make sure that pawn cant move double again
			MoveStrategy = new PawnAdapterStrategy(MoveCount, Direction, ChessBoard);
		}
		/// <summary>
		/// Specifically For en-passant 
		/// (need to remove the pawn which is currently in the opposite direction)
		/// </summary>
		/// <param name="destination"></param>
		public override void HandleCapture(Tuple<int, int> destination)
		{
			if ((destination.Equals((MoveStrategy as PawnAdapterStrategy).DiagonalLeft) || destination.Equals((MoveStrategy as PawnAdapterStrategy).DiagonalRight))
				&& ChessBoard.GetPiece(destination.Item1, destination.Item2) == null)
			{
				
				ChessBoard.Remove(destination.Item1 + (Direction * -1), destination.Item2);
			}
		}
		public override void Accept(IChessPieceVisitor visitor) { visitor.Visit(this); }
	}
}
