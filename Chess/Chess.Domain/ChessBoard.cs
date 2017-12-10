using System;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Domain
{
    public static class ChessBoardManager
    {
        private static ChessPiece[,] _instance;

		public static ChessPiece[,] Board
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

		public static void AddToBoard<T>(ChessPiece<T> piece, int row, int column) where T : ChessPiece<T>
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

		public static void RemoveFromBoard(int row, int column)
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
    }
}
