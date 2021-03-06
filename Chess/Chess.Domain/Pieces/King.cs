﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Domain
{
	public sealed class King : ChessPiece
	{
		public bool IsInCheck { get; set; }
		public King(PieceColor color, ChessBoard board) : base(color, board)
		{
			MoveStrategy = new KingMoveStrategy(board);
		}
		public static IEnumerable<Tuple<int, int>> PossibleStartingPositions(PieceColor color)
		{
			if (color == PieceColor.Black)
			{
				yield return new Tuple<int, int>(0, 4);
			}
			else
			{
				yield return new Tuple<int, int>(ChessConstants.MAX_BOARD_ROWS - 1, 4);
			}
		}
		//TODO: What determines a stalemate vs checkmate?
		public bool IsCheckMated()
		{
			return IsInCheck && !ChessBoard.CanDefendKing(PieceColor);
		}

		public override void Accept(IChessPieceVisitor visitor) { visitor.Visit(this); }
	}
}
