using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Domain
{
	public class PawnAdapterStrategy : BaseMoveStrategy, IPawnAdapter
	{
		public int MoveCount { get; private set; }
		public int Direction { get; private set; }
		public PawnAdapterStrategy(int moveCount, int direction)
		{
			MoveCount = moveCount;
			Direction = direction;
		}
		
		public override IEnumerable<Tuple<int, int>> GetMoveSet(int row, int col, PieceColor opposingPlayer)
		{
			base.GetMoveSet(row, col, opposingPlayer);
			var allPawnMoves = PawnAdvanceMoves().Concat(PawnCaptureMoves());
			return allPawnMoves;
		}

		public Tuple<int, int> DiagonalLeft
		{
			get
			{
				return new Tuple<int, int>((1 * Direction) + CurrentRow, CurrentColumn - 1);
			}
		}

		public Tuple<int, int> DiagonalRight
		{
			get
			{
				return new Tuple<int, int>((1 * Direction) + CurrentRow, CurrentColumn + 1);
			}
		}

		public Tuple<int, int> DoubleForward
		{
			get
			{
				return new Tuple<int, int>((2 * Direction) + CurrentRow, CurrentColumn);
			}
		}

		public IEnumerable<Tuple<int, int>> PawnAdvanceMoves()
		{
			var isNextRowOccupied = ChessBoard.GetPiece(CurrentRow + Direction, CurrentColumn) != null;
			var isNextNextRowOccupied = ChessBoard.GetPiece(DoubleForward.Item1, CurrentColumn) != null;
			if (!isNextRowOccupied)
			{
				yield return new Tuple<int, int>(CurrentRow + Direction, CurrentColumn);
			}
			if (MoveCount == 0 && !isNextRowOccupied && !isNextNextRowOccupied)
			{
				yield return new Tuple<int, int>(DoubleForward.Item1, CurrentColumn);
			}
		}

		public IEnumerable<Tuple<int, int>> PawnCaptureMoves()
		{
			var isDiagRightCapturable = ChessBoard.GetPiece(DiagonalRight.Item1, DiagonalRight.Item2) != null
				&& ChessBoard.GetPiece(DiagonalRight.Item1, DiagonalRight.Item2).PieceColor == OpposingColor;

			var isDiagLeftCapturable = ChessBoard.GetPiece(DiagonalLeft.Item1, DiagonalLeft.Item2) != null
				&& ChessBoard.GetPiece(DiagonalLeft.Item1, DiagonalLeft.Item2).PieceColor == OpposingColor;

			var leftAdjacent = ChessBoard.GetPiece(CurrentRow, CurrentColumn - 1);

			var isDiagLeftPassantable = DeterminePassantable(DiagonalLeft, leftAdjacent);

			var rightAdjacent = ChessBoard.GetPiece(CurrentRow, CurrentColumn + 1);

			var isDiagRightPassantable = DeterminePassantable(DiagonalRight, rightAdjacent);

			if (isDiagRightCapturable || isDiagRightPassantable)
			{
				yield return DiagonalRight;
			}
			if (isDiagLeftCapturable || isDiagLeftPassantable)
			{
				yield return DiagonalLeft;
			}
		}

		private bool DeterminePassantable(Tuple<int, int> attackDirection, ChessPiece adjacentEnemy)
		{
			var isPassantable = adjacentEnemy != null && ChessBoard.GetPiece(attackDirection.Item1, attackDirection.Item2) == null
				&& adjacentEnemy.GetType() == this.GetType() && adjacentEnemy.PieceColor == OpposingColor && (adjacentEnemy as Pawn).MoveCount == 1
				&& (adjacentEnemy as Pawn).HasMovedDouble;
			return isPassantable;
		}
	}
}
