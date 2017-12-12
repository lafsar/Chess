using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Domain
{
	public class RowMoveStrategy : BaseMoveStrategy
	{
		public override IEnumerable<Tuple<int, int>> GetMoveSet(int row, int col, PieceColor opposingPlayer)
		{
			base.GetMoveSet(row, col, opposingPlayer);
			return AllRowMoves;
		}

		public IEnumerable<Tuple<int, int>> AllRowMoves
		{
			get
			{
				return UpMoves
				.Concat(DownMoves);
			}
		}

		public IEnumerable<Tuple<int, int>> UpMoves
		{
			get
			{
				return RowDirections(1);
			}
		}

		public IEnumerable<Tuple<int, int>> DownMoves
		{
			get
			{
				return RowDirections(-1);
			}
		}

		protected IEnumerable<Tuple<int, int>> RowDirections(int rowDir)
		{
			for (int i = 1, nextRow = CurrentRow + (i * rowDir);
					ChessBoard.IsLegalBoardPosition(nextRow, CurrentColumn) && !IsLocationBlocked(nextRow, CurrentColumn);
					i++)
			{
				var nextLocation = new Tuple<int, int>(nextRow, CurrentColumn);
				yield return nextLocation;
				if (CaptureableLocation.Equals(nextLocation))
				{
					yield break;
				}
			}
		}
	}
}
