using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Domain
{
	public interface IPlayer
	{
		PieceColor PieceColor { get; set; }
		bool IsCurrentTurn { get; set; }
		int TurnCount { get; set; }
		void TakeTurn();
	}
}
