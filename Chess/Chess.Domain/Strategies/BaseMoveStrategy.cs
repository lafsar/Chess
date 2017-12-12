using System;
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
		//TODO: expose all capture locations that match the enemy King piece to determine Checked state somehow?
		public Tuple<int, int> CaptureableLocation { get; private set; }
		public virtual IEnumerable<Tuple<int, int>> GetMoveSet(int row, int col, PieceColor opposingPlayer)
		{
			CurrentRow = row;
			CurrentColumn = col;
			OpposingColor = opposingPlayer;

			return new List<Tuple<int, int>>().AsEnumerable();
		}

		public bool IsLocationBlocked(int row, int col)
		{
			var boardItem = ChessBoard.GetPiece(row, col);
			TryCapture(row, col, boardItem);
			return boardItem != null && boardItem.PieceColor != OpposingColor;
		}

		private void TryCapture(int row, int col, ChessPiece boardItem)
		{
			CaptureableLocation = boardItem != null && boardItem.PieceColor == OpposingColor
							? new Tuple<int, int>(row, col)
							: null;
		}
	}
}
