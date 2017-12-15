using System;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Domain
{
    public class ChessBoard : IChessPieceVisitor
    {
        private ChessPiece[,] _boardInstance;

		private ChessPiece[,] Board
        {
            get
            {
                if (_boardInstance == null)
                {
                    _boardInstance = new ChessPiece[ChessConstants.MAX_BOARD_ROWS, ChessConstants.MAX_BOARD_COLUMNS];
                }
                return _boardInstance;
            }
        }

		public List<Tuple<int, int>> BlackCapturableLocations { get; private set; }
		public List<Tuple<int, int>> BlackMoveLocations { get; private set; }

		public List<Tuple<int, int>> WhiteCapturableLocations { get; private set; }
		public List<Tuple<int, int>> WhiteMoveLocations { get; private set; }

		public List<ChessPiece> BlackPiecesOnBoard { get; set; }
		public List<ChessPiece> WhitePiecesOnBoard { get; set; }

		public King BlackKing { get; private set; }
		public King WhiteKing { get; private set; }

		public void UpdateBoardState()
		{
			BlackCapturableLocations = new List<Tuple<int, int>>();
			BlackMoveLocations = new List<Tuple<int, int>>();
			WhiteCapturableLocations = new List<Tuple<int, int>>();
			WhiteMoveLocations = new List<Tuple<int, int>>();
			BlackPiecesOnBoard = new List<ChessPiece>();
			WhitePiecesOnBoard = new List<ChessPiece>();
			for (var i = 0; i < Board.GetLength(0); i++)
			{
				for (var j = 0; j < Board.GetLength(1); j++)
				{
					var piece = GetPiece(i, j);
					if (piece != null)
					{
						var row = piece.Row;

						var col = piece.Column;
						piece.Accept(this);
						switch (PieceType)
						{
							case "Pawn":
								piece.MoveStrategy = new PawnAdapterStrategy(piece.MoveCount, (piece as Pawn).Direction, this);
								break;
							case "Rook":
								piece.MoveStrategy = new RookMoveStrategy(this);
								break;
							case "Knight":
								piece.MoveStrategy = new KnightMoveStrategy(this);
								break;
							case "Bishop":
								piece.MoveStrategy = new DiagonalMoveStrategy(this);
								break;
							case "Queen":
								piece.MoveStrategy = new QueenMoveStrategy(this);
								break;
							case "King":
								piece.MoveStrategy = new KingMoveStrategy(this);
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
						AddLocations(piece, row, col);
					}
				}
			}
			if (WhiteKing != null)
			{ 
				WhiteKing.IsInCheck = BlackCapturableLocations.Contains(new Tuple<int, int>(WhiteKing.Row, WhiteKing.Column));
			}
			if (BlackKing != null)
			{
				BlackKing.IsInCheck = WhiteCapturableLocations.Contains(new Tuple<int, int>(BlackKing.Row, BlackKing.Column));
			}
		}

		public bool CanDefendKing(PieceColor color)
		{
			if (BlackPiecesOnBoard != null && WhitePiecesOnBoard != null) {
				var piecesOnBoard = color == PieceColor.Black
					? BlackPiecesOnBoard
					: WhitePiecesOnBoard;
				var king = color == PieceColor.Black
					? BlackKing
					: WhiteKing;

				var canDefend = piecesOnBoard.Exists(p =>
				{
					return p.MoveStrategy.GetMoveSet(p.Row, p.Column, p.OpposingColor).ToList().Exists(m =>
					{
						return !IsCheckedState(p, m);
					});
				});
				return canDefend;
			}
			return true;
		}

		public bool IsCheckedState(ChessPiece piece, Tuple<int,int> destination)
		{
			var willBeChecked = false;
			//Check to see if next move will put us in check
			if (BlackKing != null && WhiteKing != null)
			{
				var oldBoard = Board;
				var oldRow = piece.Row;
				var oldCol = piece.Column;
				piece.BeforeMove(destination);
				Remove(oldRow, oldCol);
				piece.HandleCapture(destination);
				piece.MoveCount++;
				piece.AfterMove();
				AddReplace(piece, destination.Item1, destination.Item2);
				willBeChecked = piece.PieceColor == PieceColor.Black
					? BlackKing.IsInCheck
					: WhiteKing.IsInCheck;
				//Reset state back to what it was
				ResetBoard(oldBoard);
				piece.SetPosition(oldRow, oldCol);
				piece.MoveCount--;
				UpdateBoardState();
			}
			return willBeChecked;
		}

		private void AddLocations(ChessPiece piece, int row, int col)
		{
			if (piece.PieceColor == PieceColor.White)
			{
				var opposingColor = PieceColor.Black;

				WhiteMoveLocations.AddRange(piece.MoveStrategy.GetMoveSet(row, col, opposingColor).ToList());
				WhiteCapturableLocations.AddRange(piece.MoveStrategy.GetCapturable());
				WhitePiecesOnBoard.Add(piece);
			}
			else
			{
				var opposingColor = PieceColor.White;
				BlackMoveLocations.AddRange(piece.MoveStrategy.GetMoveSet(row, col, opposingColor).ToList());
				BlackCapturableLocations.AddRange(piece.MoveStrategy.GetCapturable());
				BlackPiecesOnBoard.Add(piece);
			}
		}

		public ChessPiece GetPiece(int row, int col)
		{
			return IsLegalBoardPosition(row, col)
				? Board[row, col]
				: null;
		}

		public void AddReplace(ChessPiece piece, int row, int column)
		{
			if (IsLegalBoardPosition(row, column)) {
				Board[row, column] = piece;
				piece.SetPosition(row, column);
				UpdateBoardState();
			}
		}

		public void Remove(int row, int column)
		{
			if (IsLegalBoardPosition(row, column))
			{
				Board[row, column] = null;
				//UpdateBoardState();
			}
		}

		public bool IsLegalBoardPosition(int row, int column)
        {
            return row >= 0 && column >= 0 && row < ChessConstants.MAX_BOARD_ROWS && column < ChessConstants.MAX_BOARD_COLUMNS;
        }

		public void ResetBoard(ChessPiece[,] oldBoard = null)
		{
			_boardInstance = oldBoard ?? new ChessPiece[ChessConstants.MAX_BOARD_ROWS, ChessConstants.MAX_BOARD_COLUMNS];
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
				piece.Accept(this);
				switch (PieceType) {
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
			var currentRow = color == PieceColor.Black
				? 0
				: ChessConstants.MAX_BOARD_ROWS - 1;
			var requiredKing = GetPiece(currentRow, 4);

			var hasKing = requiredKing != null && requiredKing.GetType().ToString() == "King" && requiredKing.MoveCount == 0 && !(requiredKing as King).IsInCheck;
			var directionColumn = direction == 1
				? ChessConstants.MAX_BOARD_COLUMNS - 1
				: 0;
			var requiredRook = color == PieceColor.Black
				? GetPiece(0, directionColumn)
				: GetPiece(ChessConstants.MAX_BOARD_ROWS - 1, directionColumn);
			var hasRook = requiredRook != null && requiredRook.GetType().ToString() == "Rook" && requiredRook.MoveCount == 0;
			var hasNoBlocks = direction == 1
				? GetPiece(currentRow, 5) == null && GetPiece(currentRow, 6) == null
				: GetPiece(currentRow, 1) == null && GetPiece(currentRow, 2) == null && GetPiece(currentRow, 3) == null;
			var destination = new Tuple<int, int>(currentRow, directionColumn);
			return hasKing && hasRook && hasNoBlocks && !IsCheckedState(requiredKing, destination);
		}

		private bool SwitchPiece(ChessPiece oldPiece, ChessPiece newPiece)
		{
			AddReplace(newPiece, oldPiece.Row, oldPiece.Column);
			return true;
		}

		private void SetupPawns()
		{
			Pawn.PossibleStartingPositions(PieceColor.Black).ToList().ForEach(p =>
			{
				var peice = new Pawn(PieceColor.Black, this);
				AddReplace(peice, p.Item1, p.Item2);
			});
			Pawn.PossibleStartingPositions(PieceColor.White).ToList().ForEach(p =>
			{
				var peice = new Pawn(PieceColor.White, this);
				AddReplace(peice, p.Item1, p.Item2);
			});
		}
		private void SetupBishops()
		{
			Bishop.PossibleStartingPositions(PieceColor.Black).ToList().ForEach(p =>
			{
				var peice = new Bishop(PieceColor.Black, this);
				AddReplace(peice, p.Item1, p.Item2);
			});
			Bishop.PossibleStartingPositions(PieceColor.White).ToList().ForEach(p =>
			{
				var peice = new Bishop(PieceColor.White, this);
				AddReplace(peice, p.Item1, p.Item2);
			});
		}
		private void SetupRooks()
		{
			Rook.PossibleStartingPositions(PieceColor.Black).ToList().ForEach(p =>
			{
				var peice = new Rook(PieceColor.Black, this);
				AddReplace(peice, p.Item1, p.Item2);
			});
			Rook.PossibleStartingPositions(PieceColor.White).ToList().ForEach(p =>
			{
				var peice = new Rook(PieceColor.White, this);
				AddReplace(peice, p.Item1, p.Item2);
			});
		}

		private void SetupQueens()
		{
			Queen.PossibleStartingPositions(PieceColor.Black).ToList().ForEach(p =>
			{
				var peice = new Queen(PieceColor.Black, this);
				AddReplace(peice, p.Item1, p.Item2);
			});
			Queen.PossibleStartingPositions(PieceColor.White).ToList().ForEach(p =>
			{
				var peice = new Queen(PieceColor.White, this);
				AddReplace(peice, p.Item1, p.Item2);
			});
		}

		private void SetupKings()
		{
			King.PossibleStartingPositions(PieceColor.Black).ToList().ForEach(p =>
			{
				var peice = new King(PieceColor.Black, this);
				AddReplace(peice, p.Item1, p.Item2);
			});
			King.PossibleStartingPositions(PieceColor.White).ToList().ForEach(p =>
			{
				var peice = new King(PieceColor.White, this);
				AddReplace(peice, p.Item1, p.Item2);
			});
		}

		private void SetupKnights()
		{
			Knight.PossibleStartingPositions(PieceColor.Black).ToList().ForEach(p =>
			{
				var peice = new Knight(PieceColor.Black, this);
				AddReplace(peice, p.Item1, p.Item2);
			});
			Knight.PossibleStartingPositions(PieceColor.White).ToList().ForEach(p =>
			{
				var peice = new Knight(PieceColor.White, this);
				AddReplace(peice, p.Item1, p.Item2);
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

		private string PieceType;

		public void Visit(Pawn pawn)
		{
			PieceType = "Pawn";
		}
		public void Visit(Rook rook)
		{
			PieceType = "Rook";
		}
		public void Visit(Knight knight)
		{
			PieceType = "Knight";
		}
		public void Visit(Bishop bishop)
		{
			PieceType = "Bishop";
		}
		public void Visit(Queen queen)
		{
			PieceType = "Queen";
		}
		public void Visit(King king)
		{
			PieceType = "King";
		}
	}
}
