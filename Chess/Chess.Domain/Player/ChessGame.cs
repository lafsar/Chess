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
		public int TurnCount { get; set; }
		private bool _isGameStarted { get; set; }
		public PieceColor Winner = PieceColor.None;
		public bool IsDraw = false;
		public void GiveNextTurn(Player NextPlayer)
		{
			
			TurnCount++;
			Player2.IsCurrentTurn = NextPlayer == Player1;
			Player1.IsCurrentTurn = NextPlayer == Player2;
			if (TurnCount >= 4)
			{

				Verify_CheckMate();
				Verify_Draw();
			}
		}

		public void Verify_CheckMate()
		{
			Winner = ChessBoard.BlackKing.IsCheckMated()
				? PieceColor.White
				: ChessBoard.WhiteKing.IsCheckMated()
				? PieceColor.Black
				: PieceColor.None;
			if (Winner != PieceColor.None)
			{
				Player2.IsCurrentTurn = Player1.IsCurrentTurn = false;
			}
		}
		public void Verify_Draw()
		{
			if (TurnCount >= 50)
			{
				Player2.IsCurrentTurn = Player1.IsCurrentTurn = false;
				IsDraw = true;
			}
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
