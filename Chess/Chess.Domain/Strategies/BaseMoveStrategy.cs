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
		public List<Tuple<int, int>> CaptureableLocation { get; protected set; }
		public virtual IEnumerable<Tuple<int, int>> GetMoveSet(int row, int col, PieceColor opposingPlayer)
		{
			CurrentRow = row;
			CurrentColumn = col;
			OpposingColor = opposingPlayer;

			return new List<Tuple<int, int>>();
		}

		public bool IsLocationBlocked(int row, int col)
		{
			var boardItem = ChessBoard.GetPiece(row, col);
			RaiseCapture(row, col, boardItem);
			return boardItem != null && boardItem.PieceColor != OpposingColor;
		}

		private void RaiseCapture(int row, int col, ChessPiece boardItem)
		{
			CaptureableLocation = new List<Tuple<int, int>>();
			if (boardItem != null && boardItem.PieceColor == OpposingColor)
			{
				CaptureableLocation.Add(new Tuple<int, int>(row, col));
			}
		}
	}
}
