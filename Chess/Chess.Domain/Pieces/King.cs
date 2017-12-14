using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Domain
{
	public sealed class King : ChessPiece
	{
		public bool IsInCheck { get; set; }
		public King(PieceColor color, ChessBoard board) : base(color, board)
		{
			MoveStrategy = new KingMoveStrategy(board);
		}
		public static IEnumerable<Tuple<int, int>> PossibleStartingPositions(PieceColor color)
		{
			if (color == PieceColor.Black)
			{
				yield return new Tuple<int, int>(0, 4);
			}
			else
			{
				yield return new Tuple<int, int>(ChessConstants.MAX_BOARD_ROWS - 1, 4);
			}
		}

		public bool IsCheckMated()
		{
			return IsInCheck && !(MoveStrategy as KingMoveStrategy).GetMoveSet(Row, Column, OpposingColor).Any() && !CanDefendTheKing();
		}

		private bool CanDefendTheKing()
		{
			var locations = PieceColor == PieceColor.Black
				? ChessBoard.BlackMoveLocations
				: ChessBoard.WhiteMoveLocations;

			return locations.Exists(l =>
			{
				ChessBoard.UpdateBoardState(l);
				var king = PieceColor == PieceColor.Black
					? ChessBoard.BlackKing
					: ChessBoard.WhiteKing;
				return !king.IsInCheck;
			});
		}
	}
}
