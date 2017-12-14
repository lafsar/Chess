using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Domain
{
	public class KnightMoveStrategy : BaseMoveStrategy
	{
		public KnightMoveStrategy(ChessBoard board) : base(board)
		{
		}

		public override IEnumerable<Tuple<int, int>> GetMoveSet(int row, int col, PieceColor opposingPlayer)
		{
			base.GetMoveSet(row, col, opposingPlayer);
			return AllKnightMoves.Where(m => CanMove(m));
		}

		private bool CanMove(Tuple<int, int> move)
		{
			return ChessBoard.IsLegalBoardPosition(move.Item1, move.Item2) && !IsLocationBlocked(move);
		}

		public List<Tuple<int, int>> AllKnightMoves
		{
			get
			{
				return new List<Tuple<int, int>>()
				{
					new Tuple<int, int>(CurrentRow + 2, CurrentColumn + 1),
					new Tuple<int, int>(CurrentRow + 1, CurrentColumn + 2),
					new Tuple<int, int>(CurrentRow - 1, CurrentColumn + 2),
					new Tuple<int, int>(CurrentRow - 2 , CurrentColumn + 1),
					new Tuple<int, int>(CurrentRow - 2, CurrentColumn - 1),
					new Tuple<int, int>(CurrentRow - 1, CurrentColumn - 2),
					new Tuple<int, int>(CurrentRow + 1, CurrentColumn - 2),
					new Tuple<int, int>(CurrentRow + 2, CurrentColumn - 1)
				};		
			}
		}
	}
}
