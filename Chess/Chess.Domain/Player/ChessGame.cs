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
		public ChessBoard ChessBoard { get; set; }
		//TODO: Chessgame declares draw if turncount = 50 and report the highest score
		public int TurnCount { get; set; }
		private bool _isGameStarted { get; set; }
		public void GiveNextTurn(Player NextPlayer)
		{
			Player2.IsCurrentTurn = NextPlayer == Player1;
			Player1.IsCurrentTurn = NextPlayer == Player2;
			TurnCount++;
		}

		public void StartGame(ChessBoard board, FirstPlayer player1, SecondPlayer player2)
		{
			ChessBoard = board;
			ChessBoard.ResetBoard();
			Player1 = player1;
			Player2 = player2;
			Player1.ChessBoard = board;
			Player2.ChessBoard = board;
			if (Player1.PieceColor == Player2.PieceColor)
			{
				Player1.PieceColor = Player1.PieceColor == PieceColor.White
					? PieceColor.Black
					: PieceColor.White;
				Player1.IsCurrentTurn = Player1.PieceColor == PieceColor.White;
				Player2.IsCurrentTurn = Player2.PieceColor == PieceColor.White;
			}

			ChessBoard.SetupAllPieces();
			_isGameStarted = true;
		}

		public bool IsGameStarted()
		{
			return _isGameStarted;
		}
	}
}
