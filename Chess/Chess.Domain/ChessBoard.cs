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
			if (GetPiece(row, column) == null)
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
			if (GetPiece(row, column) != null)
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

		public static bool PromotePawn(int row, int col, PieceColor color, string type)
		{
			var piece = ChessBoard.GetPiece(row, col);
			var requiredRow = color == PieceColor.Black
				? ChessConstants.MAX_BOARD_ROWS - 1
				: 0;
			ChessPiece newPiece;
			bool hasPromoted = false;
			if (piece != null && row == requiredRow && piece.PieceColor == color && piece.GetType() == typeof(Pawn))
			{
				switch (type) {
					case "Queen":
						newPiece = new Queen(color);
						hasPromoted = SwitchPiece(piece, newPiece);
						break;
					case "Bishop":
						newPiece = new Bishop(color);
						hasPromoted = SwitchPiece(piece, newPiece);
						break;
					case "Knight":
						newPiece = new Knight(color);
						hasPromoted = SwitchPiece(piece, newPiece);
						break;
					case "Rook":
						newPiece = new Rook(color);
						hasPromoted = SwitchPiece(piece, newPiece);
						break;
					default:
						break;
				}
			}
			return hasPromoted;
		}
		/// <summary>
		/// Castle the king for the current color
		/// </summary>
		/// <param name="color"></param>
		/// <param name="direction">
		/// 1 or -1  (-1 to the left, 1 to the right)
		/// </param>
		/// <returns>
		/// </returns>
		public static bool CastleKing(PieceColor color, int direction)
		{
			var requiredKing = color == PieceColor.Black
				? ChessBoard.GetPiece(0, 4)
				: ChessBoard.GetPiece(ChessConstants.MAX_BOARD_ROWS - 1, 4);

			var isKing = requiredKing != null && requiredKing.GetType() == typeof(King) && requiredKing.MoveCount == 0 && !(requiredKing as King).IsInCheck;
			var directionColumn = direction == 1
				? ChessConstants.MAX_BOARD_COLUMNS - 1
				: 0;
			var requiredRook = color == PieceColor.Black
				? ChessBoard.GetPiece(0, directionColumn)
				: ChessBoard.GetPiece(ChessConstants.MAX_BOARD_ROWS - 1, directionColumn);
			var isRook = requiredRook != null && requiredRook.GetType() == typeof(Rook) && requiredRook.MoveCount == 0;
			//var hasNoBlocks = dire
			return false;
		}

		private static bool SwitchPiece(ChessPiece oldPiece, ChessPiece newPiece)
		{
			ChessBoard.RemovePiece(oldPiece.Row, oldPiece.Column);
			ChessBoard.AddPiece(newPiece, oldPiece.Row, oldPiece.Column);
			return true;
		}

		private static void SetupPawns()
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
		private static void SetupBishops()
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
		private static void SetupRooks()
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

		private static void SetupQueens()
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

		private static void SetupKings()
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

		private static void SetupKnights()
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
