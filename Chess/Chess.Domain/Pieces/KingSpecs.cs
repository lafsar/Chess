using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Domain.Pieces
{
	[TestFixture]
	public class When_black_king_is_in_a_precarious_position
	{
		private King _king1;
		private King _king2;
		private Queen _queen;
		private ChessBoard _chessBoard;

		[SetUp]
		public void SetUp()
		{

			_chessBoard = new ChessBoard();
			_queen = new Queen(PieceColor.White, _chessBoard);
			_king1 = new King(PieceColor.White, _chessBoard);
			_king2 = new King(PieceColor.Black, _chessBoard);
			_chessBoard.ResetBoard();
		}

		[Test]
		public void _black_king_was_in_check_then_got_out_of_check()
		{
			_chessBoard.AddReplace(_king1, 7, 7);
			_chessBoard.AddReplace(_king2, 0, 3);
			_chessBoard.AddReplace(_queen, 1, 3);
			Assert.That(_king2.Row, Is.EqualTo(0));
			Assert.That(_king2.Column, Is.EqualTo(3));
			Assert.That(_king2.IsInCheck, Is.EqualTo(true));
			_king2.Move(1, 3);
			Assert.That(_king2.Row, Is.EqualTo(1));
			Assert.That(_king2.Column, Is.EqualTo(3));
			Assert.That(_king2.IsInCheck, Is.EqualTo(false));
			Assert.That(_king2.IsCheckMated(), Is.EqualTo(false));
		}

		[Test]
		public void _black_king_is_in_check_mate()
		{

			_chessBoard.AddReplace(_king1, 2, 2);
			_chessBoard.AddReplace(_king2, 0, 3);
			_chessBoard.AddReplace(_queen, 1, 3);
			Assert.That(_king2.IsCheckMated(), Is.EqualTo(true));
		}
	}
}
