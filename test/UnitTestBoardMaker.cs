using BoggleShared;
using FluentAssertions;
using System;
using Xunit;

namespace BoggleTests
{
    public class UnitTestBoardMaker
    {
        /// <summary>
        /// Create a random board of sideLength.
        /// </summary>
        /// <param name="sideLength">The size of a single side of the board.</param>
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(4)]
        [InlineData(6)]
        [InlineData(8)]
        [InlineData(16)]
        [InlineData(64)]
        [InlineData(1024)]
        public void TestCreateRandomBoard(int sideLength)
        {
            BoardMaker maker = new BoardMaker();
            maker.LoadRandomBoard(sideLength);

            var board = maker.GetBoard();
            board.Chars.Length.Should().Be(sideLength * sideLength);
        }

        /// <summary>
        /// Tests invalid board sizes.
        /// </summary>
        /// <param name="sideLength">The size of a single side of the board.</param>
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1025)]
        public void TestCreateRandomBoardFailure(int sideLength)
        {
            BoardMaker board = new BoardMaker();
            try
            {
                board.LoadRandomBoard(sideLength);
            }
            catch (Exception ex)
            {
                ex.Should().NotBeNull();
                board = null;
            }
            board.Should().BeNull();
        }
    }
}
