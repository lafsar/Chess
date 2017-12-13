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

		public QueenMoveStrategy()
		{
			DiagonalStrategies = new DiagonalMoveStrategy();
			RowStrategies = new RowMoveStrategy();
			ColumnStrategies = new ColumnMoveStrategy();
		}
		public override IEnumerable<Tuple<int, int>> GetMoveSet(int row, int col, PieceColor opposingPlayer)
		{
			//base.GetMoveSet(row, col, opposingPlayer);

			var strats = DiagonalStrategies.GetMoveSet(row, col, opposingPlayer)
				.Concat(RowStrategies.GetMoveSet(row, col, opposingPlayer))
				.Concat(ColumnStrategies.GetMoveSet(row, col, opposingPlayer));

			CaptureableLocation = new List<Tuple<int, int>>();
			CaptureableLocation.AddRange(DiagonalStrategies.CaptureableLocation);
			CaptureableLocation.AddRange(RowStrategies.CaptureableLocation);
			CaptureableLocation.AddRange(ColumnStrategies.CaptureableLocation);

			return strats;
		}
	}
}
