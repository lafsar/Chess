using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Domain
{
	public class DiagonalMoveStrategy : BaseMoveStrategy
	{
		public DiagonalMoveStrategy(ChessBoard board) : base(board) { }
		public override IEnumerable<Tuple<int, int>> GetMoveSet(int row, int col, PieceColor opposingPlayer)
		{
			base.GetMoveSet(row, col, opposingPlayer);
			return AllDiagonalMoves;
		}

		public override List<Tuple<int, int>> GetCapturable()
		{
			return base.GetCapturable();
		}

		public List<Tuple<int, int>> AllDiagonalMoves
		{
			get
			{
				return DiagonalUpRightMoves
				.Concat(DiagonalUpLeftMoves)
				.Concat(DiagonalDownLeftMoves)
				.Concat(DiagonalDownRightMoves).ToList();
			}
		}

		public IEnumerable<Tuple<int, int>> DiagonalUpRightMoves
		{
			get
			{
				return DiagDirections(1, 1);
			}
		}

		public IEnumerable<Tuple<int, int>> DiagonalUpLeftMoves
		{
			get
			{
				return DiagDirections(1, -1);
			}
		}

		public IEnumerable<Tuple<int, int>> DiagonalDownLeftMoves
		{
			get
			{
				return DiagDirections(-1, -1);
			}
		}

		public IEnumerable<Tuple<int, int>> DiagonalDownRightMoves
		{
			get
			{
				return DiagDirections(-1, 1);
			}
		}

		private IEnumerable<Tuple<int, int>> DiagDirections(int rowDir, int colDir)
		{
			for (int i = 1, j = 1, nextRow = NextPosition(CurrentRow, i, rowDir), nextCol = NextPosition(CurrentColumn, j, colDir);//var declarations
					ChessBoard.IsLegalBoardPosition(nextRow, nextCol) && !IsLocationBlocked(nextRow, nextCol); //terminating conditions
					i++, j++, nextRow = NextPosition(CurrentRow, i, rowDir), nextCol = NextPosition(CurrentColumn, j, colDir)) //iterators
			{
				var nextLocation = new Tuple<int, int>(nextRow, nextCol);
				yield return nextLocation;
				if (CaptureableLocation.Equals(nextLocation))
				{
					yield break;
				}
			}
		}
	}
}
