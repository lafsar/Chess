using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Domain
{
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

		protected override void AfterMove()
		{
			var destination = new Tuple<int, int>(Row, Column);
			HasMovedDouble = MoveCount == 0 && destination.Equals((MoveStrategy as PawnAdapterStrategy).DoubleForward);
			MoveStrategy = new PawnAdapterStrategy(MoveCount, Direction, ChessBoard);
		}

		protected override void HandleCapture(Tuple<int, int> destination)
		{
			if (destination.Equals((MoveStrategy as PawnAdapterStrategy).DiagonalLeft) || destination.Equals((MoveStrategy as PawnAdapterStrategy).DiagonalRight))
			{
				if (ChessBoard.GetPiece(destination.Item1, destination.Item2) != null)
				{
					ChessBoard.RemovePiece(destination.Item1, destination.Item2);
				}
				else
				{
					//For en-passant
					ChessBoard.RemovePiece(destination.Item1 + Direction, destination.Item2);
				}
			}
		}
	}
}
