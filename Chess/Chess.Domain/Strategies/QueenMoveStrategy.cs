using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Domain
{
	public class QueenMoveStrategy : BaseMoveStrategy
	{
		private DiagonalMoveStrategy DiagonalStrategies { get; set; }
		private RowMoveStrategy RowStrategies { get; set; }
		private ColumnMoveStrategy ColumnStrategies { get; set; }

		public QueenMoveStrategy(ChessBoard board) : base(board)
		{
			DiagonalStrategies = new DiagonalMoveStrategy(board);
			RowStrategies = new RowMoveStrategy(board);
			ColumnStrategies = new ColumnMoveStrategy(board);
		}
		public override IEnumerable<Tuple<int, int>> GetMoveSet(int row, int col, PieceColor opposingPlayer)
		{
			var strats = DiagonalStrategies.GetMoveSet(row, col, opposingPlayer)
				.Concat(RowStrategies.GetMoveSet(row, col, opposingPlayer))
				.Concat(ColumnStrategies.GetMoveSet(row, col, opposingPlayer)).ToList();

			return strats;
		}

		public override List<Tuple<int, int>> GetCapturable()
		{
			var captures = DiagonalStrategies.GetCapturable()
				.Concat(RowStrategies.GetCapturable())
				.Concat(ColumnStrategies.GetCapturable()).ToList();
			return captures;
		}
	}
}
