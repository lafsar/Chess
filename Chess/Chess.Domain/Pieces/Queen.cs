using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Domain
{
	public sealed class Queen : ChessPiece
	{
		public Queen(PieceColor color, ChessBoard board) : base(color, board)
		{
			MoveStrategy = new QueenMoveStrategy(board);
		}
		public static IEnumerable<Tuple<int, int>> PossibleStartingPositions(PieceColor color)
		{
			if (color == PieceColor.Black)
			{
				yield return new Tuple<int, int>(0, 3);
			}
			else
			{
				yield return new Tuple<int, int>(ChessConstants.MAX_BOARD_ROWS - 1, 3);
			}
		}
		public override void Accept(IChessPieceVisitor visitor) { visitor.Visit(this); }
	}
}
