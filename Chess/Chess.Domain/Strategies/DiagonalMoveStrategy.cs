using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Domain
{
	public class DiagonalMoveStrategy: IMoveable
	{
		private ChessPiece PieceInstance { get; set; }
		public DiagonalMoveStrategy(ChessPiece pieceInstance)
		{
			PieceInstance = pieceInstance;
		}
		
	}
}
