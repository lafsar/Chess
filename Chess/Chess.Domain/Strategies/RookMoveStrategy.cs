﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Domain
{
	public class RookMoveStrategy : BaseMoveStrategy
	{
		private RowMoveStrategy RowStrategies { get; set; }
		private ColumnMoveStrategy ColumnStrategies { get; set; }

		public RookMoveStrategy()
		{
			RowStrategies = new RowMoveStrategy();
			ColumnStrategies = new ColumnMoveStrategy();
		}
		public override IEnumerable<Tuple<int, int>> GetMoveSet(int row, int col, PieceColor opposingPlayer)
		{
			//base.GetMoveSet(row, col, opposingPlayer);

			return RowStrategies.GetMoveSet(row, col, opposingPlayer)
				.Concat(ColumnStrategies.GetMoveSet(row, col, opposingPlayer));
		}
	}
}