using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Domain.Pieces
{
	[TestFixture]
	public class When_using_a_black_queen_basic_moves
	{
		private Queen _queen;
		private ChessBoard _chessBoard;

		[SetUp]
		public void SetUp()
		{

			_chessBoard = new ChessBoard();
			_queen = new Queen(PieceColor.Black, _chessBoard);
			_chessBoard.ResetBoard();
		}

		[Test]
		public void _01_placing_the_black_queen_on_Row_equals_1_and_Column_equals_3_should_place_on_the_board()
		{
			_chessBoard.AddReplace(_queen, 1, 3);
			Assert.That(_queen.Row, Is.EqualTo(1));
			Assert.That(_queen.Column, Is.EqualTo(3));
		}

		[Test]
		public void _black_queen_blocked_by_another_black_queen()
		{
			//queen cant move to occupied space when moving forward
			_chessBoard.AddReplace(_queen, 1, 3);
			var blockingqueen = new Queen(PieceColor.Black, _chessBoard);
			_chessBoard.AddReplace(blockingqueen, 2, 3);
			_queen.Move(2, 3);
			Assert.That(_queen.Row, Is.EqualTo(1));
			Assert.That(_queen.Column, Is.EqualTo(3));
		}

		[Test]
		public void _11_making_an_illegal_move_by_placing_the_black_queen_on_X_equals_1_and_Y_eqauls_3_and_moving_to_X_equals_2_and_Y_eqauls_5_should_not_move_the_queen()
		{
			//queen cant move like a knight
			_chessBoard.AddReplace(_queen, 1, 3);
			_queen.Move(2, 5);
			Assert.That(_queen.Row, Is.EqualTo(1));
			Assert.That(_queen.Column, Is.EqualTo(3));
		}

		[Test]
		public void _12_making_a_legal_move_by_placing_the_black_queen_on_X_equals_1_and_Y_eqauls_3_and_moving_to_X_equals_2_and_Y_eqauls_4_should_move_the_queen()
		{
			_chessBoard.AddReplace(_queen, 1, 3);
			_queen.Move(2, 4);
			Assert.That(_queen.Row, Is.EqualTo(2));
			Assert.That(_queen.Column, Is.EqualTo(4));
		}

		[Test]
		public void _20_making_a_legal_move_by_placing_the_black_queen_on_X_equals_1_and_Y_eqauls_3_and_moving_to_X_equals_0_and_Y_eqauls_3_should_move_the_queen()
		{

			_chessBoard.AddReplace(_queen, 1, 3);
			_queen.Move(0, 3);
			Assert.That(_queen.Row, Is.EqualTo(0));
			Assert.That(_queen.Column, Is.EqualTo(3));
		}

		[Test]
		public void _making_a_legal_move_by_placing_the_black_queen_on_X_equals_1_and_Y_eqauls_3_and_moving_to_X_equals_1_and_Y_eqauls_0_should_move_the_queen()
		{

			_chessBoard.AddReplace(_queen, 1, 3);
			_queen.Move(1, 0);
			Assert.That(_queen.Row, Is.EqualTo(1));
			Assert.That(_queen.Column, Is.EqualTo(0));
		}

		[Test]
		public void _making_a_legal_move_by_placing_the_black_queen_on_X_equals_0_and_Y_eqauls_0_and_moving_to_X_equals_7_and_Y_eqauls_7_should_move_the_queen()
		{

			_chessBoard.AddReplace(_queen, 0, 0);
			_queen.Move(7, 7);
			Assert.That(_queen.Row, Is.EqualTo(7));
			Assert.That(_queen.Column, Is.EqualTo(7));
		}

	}
}
