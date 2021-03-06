﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Domain
{
	public abstract class BaseMoveStrategy : IMoveStrategy
	{
		protected int CurrentRow { get; private set; }
		protected int CurrentColumn { get; private set; }
		protected PieceColor OpposingColor { get; private set; }
		public List<Tuple<int, int>> CaptureableLocation { get; private set; }
		protected ChessBoard ChessBoard;
		protected BaseMoveStrategy(ChessBoard board)
		{
			ChessBoard = board;
			CaptureableLocation = new List<Tuple<int, int>>();
		}
		public virtual IEnumerable<Tuple<int, int>> GetMoveSet(int row, int col, PieceColor opposingPlayer)
		{
			CurrentRow = row;
			CurrentColumn = col;
			OpposingColor = opposingPlayer;

			return new List<Tuple<int, int>>();
		}

		public virtual List<Tuple<int,int>> GetCapturable()
		{
			return CaptureableLocation;
		}

		public bool IsLocationBlocked(int row, int col)
		{
			var boardItem = ChessBoard.GetPiece(row, col);
			RaiseCapture(row, col, boardItem);
			return boardItem != null && boardItem.PieceColor != OpposingColor;

		}
		public bool IsLocationBlocked(Tuple<int, int> move)
		{
			var boardItem = ChessBoard.GetPiece(move.Item1, move.Item2);
			RaiseCapture(move.Item1, move.Item2, boardItem);
			return boardItem != null && boardItem.PieceColor != OpposingColor;
		}

		private void RaiseCapture(int row, int col, ChessPiece boardItem)
		{
			if (boardItem != null && boardItem.PieceColor == OpposingColor)
			{
				CaptureableLocation.Add(new Tuple<int, int>(row, col));
			}
		}

		public int NextPosition(int current, int magnitude, int direction)
		{
			return current + (magnitude * direction);
		}
	}
}
