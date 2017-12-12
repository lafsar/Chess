using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Domain
{
	public class ColumnMoveStrategy : BaseMoveStrategy
	{
		public override IEnumerable<Tuple<int, int>> GetMoveSet(int row, int col, PieceColor opposingPlayer)
		{
			base.GetMoveSet(row, col, opposingPlayer);
			return AllColumnMoves;
		}

		public IEnumerable<Tuple<int, int>> AllColumnMoves
		{
			get
			{
				return RightMoves
				.Concat(LeftMoves);
			}
		}

		public IEnumerable<Tuple<int, int>> RightMoves
		{
			get
			{
				return ColumnDirections(1);
			}
		}

		public IEnumerable<Tuple<int, int>> LeftMoves
		{
			get
			{
				return ColumnDirections(-1);
			}
		}

		private IEnumerable<Tuple<int, int>> ColumnDirections(int colDir)
		{
			for (int i = 1, nextCol = CurrentColumn + (i * colDir);
					ChessBoard.IsLegalBoardPosition(CurrentRow, nextCol) && !IsLocationBlocked(CurrentRow, nextCol);
					i++)
			{
				var nextLocation = new Tuple<int, int>(CurrentRow, nextCol);
				yield return nextLocation;
				if (CaptureableLocation.Equals(nextLocation))
				{
					yield break;
				}
			}
		}
	}
}
