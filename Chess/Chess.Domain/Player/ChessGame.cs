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
		public static int TurnCount { get; set; }
		public void GiveNextTurn(Player NextPlayer)
		{
			Player2.IsCurrentTurn = NextPlayer == Player1;
			Player1.IsCurrentTurn = NextPlayer == Player2;
		}

		public void StartGame(FirstPlayer player1, SecondPlayer player2)
		{
			ChessBoard.ResetBoard();
			Player1 = player1;
			Player2 = player2;
			if (Player1.PieceColor == Player2.PieceColor)
			{
				Player1.PieceColor = Player1.PieceColor == PieceColor.White
					? PieceColor.Black
					: PieceColor.White;
				Player1.IsCurrentTurn = Player1.PieceColor == PieceColor.White;
				Player2.IsCurrentTurn = Player2.PieceColor == PieceColor.White;
			}

			ChessBoard.SetupAllPieces();
		}
	}
}
