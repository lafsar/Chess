using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Domain
{
	[TestFixture]
	public class When_creating_a_chess_board
	{
		private ChessBoard _chessboard;
		[SetUp]
		public void SetUp()
		{
			_chessboard = new ChessBoard();
		}

		[Test]
		public void _001_the_playing_board_should_have_a_Max_Board_Width_of_8()
		{
			Assert.That(ChessConstants.MAX_BOARD_ROWS, Is.EqualTo(8));
		}

		[Test]
		public void _002_the_playing_board_should_have_a_Max_Board_Height_of_8()
		{
			Assert.That(ChessConstants.MAX_BOARD_COLUMNS, Is.EqualTo(8));
		}

		[Test]
		public void _005_the_playing_board_should_know_that_X_equals_0_and_Y_equals_0_is_a_valid_board_position()
		{
			var isValidPosition = _chessboard.IsLegalBoardPosition(0, 0);
			Assert.That(isValidPosition, Is.True);
		}

		[Test]
		public void _006_the_playing_board_should_know_that_X_equals_5_and_Y_equals_5_is_a_valid_board_position()
		{
			var isValidPosition = _chessboard.IsLegalBoardPosition(5, 5);
			Assert.That(isValidPosition, Is.True);
		}

		[Test]
		public void _010_the_playing_board_should_know_that_X_equals_11_and_Y_equals_5_is_an_invalid_board_position()
		{
			var isValidPosition = _chessboard.IsLegalBoardPosition(11, 5);
			Assert.That(isValidPosition, Is.False);
		}

		[Test]
		public void _011_the_playing_board_should_know_that_X_equals_0_and_Y_equals_8_is_an_invalid_board_position()
		{
			var isValidPosition = _chessboard.IsLegalBoardPosition(0, 9);
			Assert.That(isValidPosition, Is.False);
		}

		[Test]
		public void _011_the_playing_board_should_know_that_X_equals_11_and_Y_equals_0_is_an_invalid_board_position()
		{
			var isValidPosition = _chessboard.IsLegalBoardPosition(11, 0);
			Assert.That(isValidPosition, Is.False);
		}

		[Test]
		public void _012_the_playing_board_should_know_that_X_equals_minus_1_and_Y_equals_5_is_an_invalid_board_position()
		{
			var isValidPosition = _chessboard.IsLegalBoardPosition(-1, 5);
			Assert.That(isValidPosition, Is.False);
		}

		[Test]
		public void _012_the_playing_board_should_know_that_X_equals_5_and_Y_equals_minus_1_is_an_invalid_board_position()
		{
			var isValidPosition = _chessboard.IsLegalBoardPosition(5, -1);
			Assert.That(isValidPosition, Is.False);
		}
	}
}
