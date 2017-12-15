using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Domain
{
	/// <summary>
	/// Allows ChessBoard to check the ChessPiece type in a robust manner
	/// </summary>
	public interface IChessPieceVisitor
	{
		void Visit(Pawn pawn);
		void Visit(Rook rook);
		void Visit(Knight knight);
		void Visit(Bishop bishop);
		void Visit(Queen queen);
		void Visit(King king);
	}
}
