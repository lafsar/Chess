using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Domain
{
	public class KingMoveStrategy : BaseMoveStrategy
	{
		public KingMoveStrategy(ChessBoard board) : base(board)
		{
		}

		public override IEnumerable<Tuple<int, int>> GetMoveSet(int row, int col, PieceColor opposingPlayer)
		{
			base.GetMoveSet(row, col, opposingPlayer);
			var allMoves = AllKingMoves.Where(m => CanMove(m));
			return allMoves;
		}

		private bool CanMove(Tuple<int, int> movement)
		{
			var isLegal = ChessBoard.IsLegalBoardPosition(movement.Item1, movement.Item2) && !IsLocationBlocked(movement);
			var moveRestrictions = OpposingColor == PieceColor.Black
				? ChessBoard.BlackMoveLocations
				: ChessBoard.WhiteMoveLocations;
			var positionAvailable = !moveRestrictions.Contains(movement);
			return isLegal && positionAvailable;
		}

		public override List<Tuple<int, int>> GetCapturable()
		{
			return base.GetCapturable();
		}

		public List<Tuple<int, int>> AllKingMoves
		{
			get
			{
				return new List<Tuple<int, int>>()
				{
					new Tuple<int, int>(CurrentRow + 1, CurrentColumn),
					new Tuple<int, int>(CurrentRow + 1, CurrentColumn + 1),
					new Tuple<int, int>(CurrentRow, CurrentColumn + 1),
					new Tuple<int, int>(CurrentRow + 1, CurrentColumn - 1),
					new Tuple<int, int>(CurrentRow - 1, CurrentColumn -1),
					new Tuple<int, int>(CurrentRow, CurrentColumn - 1),
					new Tuple<int, int>(CurrentRow, CurrentColumn),
					new Tuple<int, int>(CurrentRow - 1, CurrentColumn + 1)
				};
			}
		}
	}
}
