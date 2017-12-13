using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using System;

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

    [TestFixture]
    public class When_using_a_black_pawn_and_basic_movement
    {
        private Pawn _pawn;
		private ChessBoard _chessBoard;

        [SetUp]
        public void SetUp()
        {

			_chessBoard = new ChessBoard();
			_pawn = new Pawn(PieceColor.Black, _chessBoard);
			_chessBoard.ResetBoard();
		}

        [Test]
        public void _01_placing_the_black_pawn_on_Row_equals_1_and_Column_equals_3_should_place_the_black_pawn_on_that_place_on_the_board()
        {
			_chessBoard.AddPiece(_pawn, 1, 3);
			Assert.That(_pawn.Row, Is.EqualTo(1));
            Assert.That(_pawn.Column, Is.EqualTo(3));
        }

        [Test]
        public void _10_making_an_illegal_move_by_placing_the_black_pawn_on_Row_equals_1_and_Column_eqauls_3_and_moving_to_Row_equals_2_and_Column_eqauls_3_should_not_move_the_pawn()
        {
			_chessBoard.AddPiece(_pawn, 1, 3);
			var blockingPawn = new Pawn(PieceColor.Black, _chessBoard);
			_chessBoard.AddPiece(blockingPawn, 2, 3);
			_pawn.Move(2, 3);
            Assert.That(_pawn.Row, Is.EqualTo(1));
            Assert.That(_pawn.Column, Is.EqualTo(3));
        }

        [Test]
        public void _11_making_an_illegal_move_by_placing_the_black_pawn_on_X_equals_1_and_Y_eqauls_3_and_moving_to_X_equals_4_and_Y_eqauls_3_should_not_move_the_pawn()
        {
			_chessBoard.AddPiece(_pawn, 1, 3);
			_pawn.Move(2, 3);
			_pawn.Move(4, 3);
			Assert.That(_pawn.Row, Is.EqualTo(2));
            Assert.That(_pawn.Column, Is.EqualTo(3));
        }

		[Test]
		public void _12_making_a_legal_move_by_placing_the_black_pawn_on_X_equals_1_and_Y_eqauls_3_and_moving_to_X_equals_3_and_Y_eqauls_3_should_move_the_pawn()
		{

			_chessBoard.AddPiece(_pawn, 1, 3);
			_pawn.Move(3, 3);
			Assert.That(_pawn.Row, Is.EqualTo(3));
			Assert.That(_pawn.Column, Is.EqualTo(3));
		}

		[Test]
        public void _20_making_a_legal_move_by_placing_the_black_pawn_on_X_equals_1_and_Y_eqauls_3_and_moving_to_X_equals_2_and_Y_eqauls_3_should_move_the_pawn()
        {

			_chessBoard.AddPiece(_pawn, 1, 3);
            _pawn.Move(2, 3);
            Assert.That(_pawn.Row, Is.EqualTo(2));
            Assert.That(_pawn.Column, Is.EqualTo(3));
        }

    }

    [TestFixture]
    public class When_using_a_white_pawn_and
    {
		private Pawn _pawn;
		private ChessBoard _chessBoard;

		[SetUp]
        public void SetUp()
        {
			_chessBoard = new ChessBoard();
			_chessBoard.ResetBoard();
			_pawn = new Pawn(PieceColor.White, _chessBoard);
			
		}

        [Test]
        public void _01_placing_the_white_pawn_on_X_equals_6_and_Y_equals_1_should_place_the_white_pawn_on_that_place_on_the_board()
        {

			_chessBoard.AddPiece(_pawn, 6, 1);
			Assert.That(_pawn.Row, Is.EqualTo(6));
            Assert.That(_pawn.Column, Is.EqualTo(1));
        }

        [Test]
        public void _10_making_an_illegal_move_by_placing_the_white_pawn_on_X_equals_6_and_Y_eqauls_1_and_moving_to_X_equals_7_and_Y_eqauls_2_should_not_move_the_pawn()
        {
			_chessBoard.AddPiece(_pawn, 6, 1);
			_pawn.Move(7, 2);
            Assert.That(_pawn.Row, Is.EqualTo(6));
            Assert.That(_pawn.Column, Is.EqualTo(1));
        }

        [Test]
        public void _11_making_an_illegal_move_by_placing_the_white_pawn_on_X_equals_6_and_Y_eqauls_1_and_moving_to_X_equals_6_and_Y_eqauls_4_should_not_move_the_pawn()
        {
			_chessBoard.AddPiece(_pawn, 6, 1);
			_pawn.Move(6, 4);
            Assert.That(_pawn.Row, Is.EqualTo(6));
            Assert.That(_pawn.Column, Is.EqualTo(1));
        }

        [Test]
        public void _20_making_a_illegal_move_by_placing_the_white_pawn_on_X_equals_6_and_Y_eqauls_1_and_moving_to_X_equals_7_and_Y_eqauls_1_should_not_move_the_pawn()
        {
			_chessBoard.AddPiece(_pawn, 6, 1);
			_pawn.Move(7, 1);
            Assert.That(_pawn.Row, Is.EqualTo(6));
            Assert.That(_pawn.Column, Is.EqualTo(1));
        }

    }

}
