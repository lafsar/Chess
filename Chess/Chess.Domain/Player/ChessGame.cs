using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Domain
{
	public class ChessGame : IPlayerMediator
	{
		public FirstPlayer Player1 { get; set; }
		public SecondPlayer Player2 { get; set; }
		public void GiveNextTurn(Player NextPlayer)
		{
			Player2.IsCurrentTurn = NextPlayer == Player1;
			Player1.IsCurrentTurn = NextPlayer == Player2;
		}

		public static int TurnCount { get; set; }
		public static void StartGame(FirstPlayer player1, SecondPlayer player2)
		{
			ChessBoard.ResetBoard();

			if (player1.PieceColor == player2.PieceColor)
			{
				player1.PieceColor = player1.PieceColor == PieceColor.White
					? PieceColor.Black
					: PieceColor.White;
				player1.IsCurrentTurn = player1.PieceColor == PieceColor.White;
				player2.IsCurrentTurn = player2.PieceColor == PieceColor.White;
			}

			//TODO: Populate board with all the pieces
		}
	}
}
