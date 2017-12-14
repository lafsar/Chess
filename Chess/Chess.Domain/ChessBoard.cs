using System;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Domain
{
    public class ChessBoard
    {
        private ChessPiece[,] _instance;

		private ChessPiece[,] Board
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

		public List<Tuple<int, int>> BlackCapturableLocations { get; private set; }
		public List<Tuple<int, int>> BlackMoveLocations { get; private set; }

		public List<Tuple<int, int>> WhiteCapturableLocations { get; private set; }
		public List<Tuple<int, int>> WhiteMoveLocations { get; private set; }


		public King BlackKing { get; private set; }
		public King WhiteKing{ get; private set; }

		public void UpdateBoardState()
		{
			BlackCapturableLocations = new List<Tuple<int, int>>();
			BlackMoveLocations = new List<Tuple<int, int>>();
			WhiteCapturableLocations = new List<Tuple<int, int>>();
			WhiteMoveLocations = new List<Tuple<int, int>>();
			for (var i = 0; i < Board.GetLength(0); i++)
			{
				for (var j = 0; j < Board.GetLength(1); j++)
				{
					var piece = GetPiece(i, j);
					if (piece != null)
					{
						var type = piece.GetType().ToString();

						switch(type)
						{
							case "Pawn":
								piece.MoveStrategy = new PawnAdapterStrategy(piece.MoveCount, (piece as Pawn).Direction, this);
								AddLocations(piece.PieceColor, piece.MoveStrategy);
								break;
							case "Rook":
								piece.MoveStrategy = new RookMoveStrategy(this);
								AddLocations(piece.PieceColor, piece.MoveStrategy);
								break;
							case "Knight":
								piece.MoveStrategy = new KnightMoveStrategy(this);
								AddLocations(piece.PieceColor, piece.MoveStrategy);
								break;
							case "Bishop":
								piece.MoveStrategy = new DiagonalMoveStrategy(this);
								AddLocations(piece.PieceColor, piece.MoveStrategy);
								break;
							case "Queen":
								piece.MoveStrategy = new QueenMoveStrategy(this);
								AddLocations(piece.PieceColor, piece.MoveStrategy);
								break;
							case "King":
								piece.MoveStrategy = new KingMoveStrategy(this);
								AddLocations(piece.PieceColor, piece.MoveStrategy);
								if (piece.PieceColor == PieceColor.White)
								{
									WhiteKing = piece as King;
								} else
								{
									BlackKing = piece as King;
								}
								break;
							default:
								break;
					
						}
					}
				}
			}
			WhiteKing.IsInCheck = BlackCapturableLocations.Contains(new Tuple<int, int>(WhiteKing.Row, WhiteKing.Column));
			BlackKing.IsInCheck = WhiteCapturableLocations.Contains(new Tuple<int, int>(BlackKing.Row, BlackKing.Column));
		}

		private void AddLocations(PieceColor color, IMoveStrategy strategy)
		{
			if (color == PieceColor.White)
			{
				WhiteMoveLocations = strategy.GetAllMoves();
				WhiteCapturableLocations = strategy.GetCapturable();
			} else
			{
				BlackCapturableLocations = strategy.GetCapturable();
				BlackMoveLocations = strategy.GetAllMoves();
			}
		}

		public ChessPiece GetPiece(int row, int col)
		{
			return IsLegalBoardPosition(row, col)
				? Board[row, col]
				: null;
		}

		public void AddPiece(ChessPiece piece, int row, int column)
		{
			if (GetPiece(row, column) == null)
			{
				Board[row, column] = piece;
				piece.SetPosition(row, column);
				UpdateBoardState();
			}
			else
			{
				throw new Exception("Not a valid position!");
			}
		}

		public void RemovePiece(int row, int column)
		{
			if (GetPiece(row, column) != null)
			{
				Board[row, column] = null;
				UpdateBoardState();
			}
		}

		public bool IsLegalBoardPosition(int row, int column)
        {
            return row >= 0 && column >= 0 && row < ChessConstants.MAX_BOARD_ROWS && column < ChessConstants.MAX_BOARD_COLUMNS;
        }

		public void ResetBoard()
		{
			if (_instance != null) { 
				_instance = new ChessPiece[ChessConstants.MAX_BOARD_ROWS - 1, ChessConstants.MAX_BOARD_COLUMNS - 1];
			}
		}

		public bool PromotePawn(int row, int col, PieceColor color, string type)
		{
			var piece = GetPiece(row, col);
			var requiredRow = color == PieceColor.Black
				? ChessConstants.MAX_BOARD_ROWS - 1
				: 0;
			ChessPiece newPiece;
			bool hasPromoted = false;
			if (piece != null && row == requiredRow && piece.PieceColor == color && piece.GetType() == typeof(Pawn))
			{
				switch (type) {
					case "Queen":
						newPiece = new Queen(color, this);
						hasPromoted = SwitchPiece(piece, newPiece);
						break;
					case "Bishop":
						newPiece = new Bishop(color, this);
						hasPromoted = SwitchPiece(piece, newPiece);
						break;
					case "Knight":
						newPiece = new Knight(color, this);
						hasPromoted = SwitchPiece(piece, newPiece);
						break;
					case "Rook":
						newPiece = new Rook(color, this);
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
		public bool CastleKing(PieceColor color, int direction)
		{
			var requiredKing = color == PieceColor.Black
				? GetPiece(0, 4)
				: GetPiece(ChessConstants.MAX_BOARD_ROWS - 1, 4);

			var isKing = requiredKing != null && requiredKing.GetType() == typeof(King) && requiredKing.MoveCount == 0 && !(requiredKing as King).IsInCheck;
			var directionColumn = direction == 1
				? ChessConstants.MAX_BOARD_COLUMNS - 1
				: 0;
			var requiredRook = color == PieceColor.Black
				? GetPiece(0, directionColumn)
				: GetPiece(ChessConstants.MAX_BOARD_ROWS - 1, directionColumn);
			var isRook = requiredRook != null && requiredRook.GetType() == typeof(Rook) && requiredRook.MoveCount == 0;
			//var hasNoBlocks = dire
			return false;
		}

		private bool SwitchPiece(ChessPiece oldPiece, ChessPiece newPiece)
		{
			RemovePiece(oldPiece.Row, oldPiece.Column);
			AddPiece(newPiece, oldPiece.Row, oldPiece.Column);
			return true;
		}

		private void SetupPawns()
		{
			Pawn.PossibleStartingPositions(PieceColor.Black).ToList().ForEach(p =>
			{
				var peice = new Pawn(PieceColor.Black, this);
				AddPiece(peice, p.Item1, p.Item2);
			});
			Pawn.PossibleStartingPositions(PieceColor.White).ToList().ForEach(p =>
			{
				var peice = new Pawn(PieceColor.White, this);
				AddPiece(peice, p.Item1, p.Item2);
			});
		}
		private void SetupBishops()
		{
			Bishop.PossibleStartingPositions(PieceColor.Black).ToList().ForEach(p =>
			{
				var peice = new Bishop(PieceColor.Black, this);
				AddPiece(peice, p.Item1, p.Item2);
			});
			Bishop.PossibleStartingPositions(PieceColor.White).ToList().ForEach(p =>
			{
				var peice = new Bishop(PieceColor.White, this);
				AddPiece(peice, p.Item1, p.Item2);
			});
		}
		private void SetupRooks()
		{
			Rook.PossibleStartingPositions(PieceColor.Black).ToList().ForEach(p =>
			{
				var peice = new Rook(PieceColor.Black, this);
				AddPiece(peice, p.Item1, p.Item2);
			});
			Rook.PossibleStartingPositions(PieceColor.White).ToList().ForEach(p =>
			{
				var peice = new Rook(PieceColor.White, this);
				AddPiece(peice, p.Item1, p.Item2);
			});
		}

		private void SetupQueens()
		{
			Queen.PossibleStartingPositions(PieceColor.Black).ToList().ForEach(p =>
			{
				var peice = new Queen(PieceColor.Black, this);
				AddPiece(peice, p.Item1, p.Item2);
			});
			Queen.PossibleStartingPositions(PieceColor.White).ToList().ForEach(p =>
			{
				var peice = new Queen(PieceColor.White, this);
				AddPiece(peice, p.Item1, p.Item2);
			});
		}

		private void SetupKings()
		{
			King.PossibleStartingPositions(PieceColor.Black).ToList().ForEach(p =>
			{
				var peice = new King(PieceColor.Black, this);
				AddPiece(peice, p.Item1, p.Item2);
			});
			King.PossibleStartingPositions(PieceColor.White).ToList().ForEach(p =>
			{
				var peice = new King(PieceColor.White, this);
				AddPiece(peice, p.Item1, p.Item2);
			});
		}

		private void SetupKnights()
		{
			Knight.PossibleStartingPositions(PieceColor.Black).ToList().ForEach(p =>
			{
				var peice = new Knight(PieceColor.Black, this);
				AddPiece(peice, p.Item1, p.Item2);
			});
			Knight.PossibleStartingPositions(PieceColor.White).ToList().ForEach(p =>
			{
				var peice = new Knight(PieceColor.White, this);
				AddPiece(peice, p.Item1, p.Item2);
			});
		}

		public void SetupAllPieces()
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
