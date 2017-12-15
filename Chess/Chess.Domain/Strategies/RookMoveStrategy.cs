using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Domain
{
	public class RookMoveStrategy : BaseMoveStrategy
	{
		private RowMoveStrategy RowStrategies { get; set; }
		private ColumnMoveStrategy ColumnStrategies { get; set; }

		public RookMoveStrategy(ChessBoard board) : base(board)
		{
			RowStrategies = new RowMoveStrategy(board);
			ColumnStrategies = new ColumnMoveStrategy(board);
		}
		public override IEnumerable<Tuple<int, int>> GetMoveSet(int row, int col, PieceColor opposingPlayer)
		{
			//base.GetMoveSet(row, col, opposingPlayer);

			var strats = RowStrategies.GetMoveSet(row, col, opposingPlayer)
				.Concat(ColumnStrategies.GetMoveSet(row, col, opposingPlayer)).ToList();

			return strats;
		}

		public override List<Tuple<int, int>> GetCapturable()
		{
			return RowStrategies.GetCapturable()
				.Concat(ColumnStrategies.GetCapturable())
				.ToList();
		}
	}
}
