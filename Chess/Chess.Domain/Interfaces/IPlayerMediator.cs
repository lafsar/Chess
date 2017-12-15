using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Domain
{
	/// <summary>
	/// The ChessGame should mediate the current player's turn
	/// So nobody cheats as long as both players subscribe to the ChessGame
	/// </summary>
	public interface IPlayerMediator
	{
		void GiveNextTurn(Player NextPlayer);
		bool IsGameStarted();
	}
}
