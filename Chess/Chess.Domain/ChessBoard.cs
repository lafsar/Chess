using System;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Domain
{
    public static class ChessBoard
    {
        private static ChessPiece[,] _instance;

		private static ChessPiece[,] Board
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ChessPiece[ChessConstants.MAX_BOARD_ROWS - 1, ChessConstants.MAX_BOARD_COLUMNS - 1];
                }
                return _instance;
            }
        }

		public static ChessPiece GetPiece(int row, int col)
		{
			return IsLegalBoardPosition(row, col)
				? Board[row, col]
				: null;
		}

		public static void AddPiece(ChessPiece piece, int row, int column)
		{
			if (IsLegalBoardPosition(row, column) && Board[row, column] == null)
			{
				Board[row, column] = piece;
				piece.SetPosition(row, column);
			}
			else
			{
				throw new Exception("Not a valid position!");
			}
		}

		public static void RemovePiece(int row, int column)
		{
			if (IsLegalBoardPosition(row, column) && Board[row, column] != null)
			{
				Board[row, column] = null;
			}
		}

		public static bool IsLegalBoardPosition(int row, int column)
        {
            return row >= 0 && column >= 0 && row < ChessConstants.MAX_BOARD_ROWS && column < ChessConstants.MAX_BOARD_COLUMNS;
        }

		public static void ResetBoard()
		{
			if (_instance != null) { 
				_instance = new ChessPiece[ChessConstants.MAX_BOARD_ROWS - 1, ChessConstants.MAX_BOARD_COLUMNS - 1];
			}
		}

		public static void PromotePawn(int row, int col, PieceColor color, string type)
		{
			var piece = ChessBoard.GetPiece(row, col);
			var requiredRow = color == PieceColor.Black
				? 0
				: ChessConstants.MAX_BOARD_ROWS - 1;
			ChessPiece newPiece;
			if (piece != null && row == requiredRow && piece.PieceColor == color && piece.GetType().ToString() == "Pawn")
			{
				switch (type) {
					case "Queen":
						//newPiece = new Queen(PieceColor);

					case "Bishop":
					case "Knight":
					case "Rook":
					case "Pawn":
						break;
				}
			}
		}

		private static void SwitchPiece(ChessPiece oldPiece, ChessPiece newPiece)
		{
			//ChessBoard.RemovePiece(oldPiece.Row, oldPiece.Column);
			//ChessBoard.
		}

		public static void SetupPawns()
		{
			Pawn.PossibleStartingPositions(PieceColor.Black).ToList().ForEach(p =>
			{
				var peice = new Pawn(PieceColor.Black);
				ChessBoard.AddPiece(peice, p.Item1, p.Item2);
			});
			Pawn.PossibleStartingPositions(PieceColor.White).ToList().ForEach(p =>
			{
				var peice = new Pawn(PieceColor.White);
				ChessBoard.AddPiece(peice, p.Item1, p.Item2);
			});
		}
		public static void SetupBishops()
		{
			Bishop.PossibleStartingPositions(PieceColor.Black).ToList().ForEach(p =>
			{
				var peice = new Bishop(PieceColor.Black);
				ChessBoard.AddPiece(peice, p.Item1, p.Item2);
			});
			Bishop.PossibleStartingPositions(PieceColor.White).ToList().ForEach(p =>
			{
				var peice = new Bishop(PieceColor.White);
				ChessBoard.AddPiece(peice, p.Item1, p.Item2);
			});
		}
		public static void SetupRooks()
		{
			Rook.PossibleStartingPositions(PieceColor.Black).ToList().ForEach(p =>
			{
				var peice = new Rook(PieceColor.Black);
				ChessBoard.AddPiece(peice, p.Item1, p.Item2);
			});
			Rook.PossibleStartingPositions(PieceColor.White).ToList().ForEach(p =>
			{
				var peice = new Rook(PieceColor.White);
				ChessBoard.AddPiece(peice, p.Item1, p.Item2);
			});
		}

		public static void SetupQueens()
		{
			Queen.PossibleStartingPositions(PieceColor.Black).ToList().ForEach(p =>
			{
				var peice = new Queen(PieceColor.Black);
				ChessBoard.AddPiece(peice, p.Item1, p.Item2);
			});
			Queen.PossibleStartingPositions(PieceColor.White).ToList().ForEach(p =>
			{
				var peice = new Queen(PieceColor.White);
				ChessBoard.AddPiece(peice, p.Item1, p.Item2);
			});
		}

		public static void SetupKings()
		{
			King.PossibleStartingPositions(PieceColor.Black).ToList().ForEach(p =>
			{
				var peice = new King(PieceColor.Black);
				ChessBoard.AddPiece(peice, p.Item1, p.Item2);
			});
			King.PossibleStartingPositions(PieceColor.White).ToList().ForEach(p =>
			{
				var peice = new King(PieceColor.White);
				ChessBoard.AddPiece(peice, p.Item1, p.Item2);
			});
		}

		public static void SetupKnights()
		{
			Knight.PossibleStartingPositions(PieceColor.Black).ToList().ForEach(p =>
			{
				var peice = new Knight(PieceColor.Black);
				ChessBoard.AddPiece(peice, p.Item1, p.Item2);
			});
			Knight.PossibleStartingPositions(PieceColor.White).ToList().ForEach(p =>
			{
				var peice = new Knight(PieceColor.White);
				ChessBoard.AddPiece(peice, p.Item1, p.Item2);
			});
		}

		public static void SetupAllPieces()
		{
			SetupPawns();
			SetupRooks();
			SetupKnights();
			SetupBishops();
			SetupQueens();
			SetupKings();
		}
	}
}
