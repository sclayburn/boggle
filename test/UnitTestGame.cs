using Boggle.Game;
using Boggle.Services;
using BoggleShared;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BoggleTests
{
    public class UnitTestGame
    {
        /// <summary>
        /// Tests the singlethreaded game solver with a simple board and known dictionary.
        /// </summary>
        [Fact]
        public void TestSimpleDefinedSinglethreadedGame()
        {
            char[] chars = new char[] { 'y', 'o', 'x', 'r', 'b', 'a', 'v', 'e', 'd' };
            BoardMaker maker = new BoardMaker();
            maker.LoadGivenBoard(chars, 3);
            Board board = maker.GetBoard();

            IWordDict dict = new WordDictMock();
            dict.LoadDictionaryFromDiskAsync();

            IGameSolver service = new SinglethreadedGameSolverService();
            var foundWords = service.Solve(dict, board);

            ValidateFoundWords(foundWords);
        }

        /// <summary>
        /// Tests the async game solver with a simple board and known dictionary.
        /// </summary>
        [Fact]
        public void TestSimpleDefinedAsyncGame()
        {
            char[] chars = new char[] { 'y', 'o', 'x', 'r', 'b', 'a', 'v', 'e', 'd' };
            BoardMaker maker = new BoardMaker();
            maker.LoadGivenBoard(chars, 3);
            Board board = maker.GetBoard();

            IWordDict dict = new WordDictMock();
            dict.LoadDictionaryFromDiskAsync();

            IGameSolver service = new AsyncGameSolverService();
            var foundWords = service.Solve(dict, board);

            ValidateFoundWords(foundWords);
        }

        private void ValidateFoundWords(IEnumerable<string> foundWords)
        {
            foundWords.Count().Should().Be(31);
            
            foundWords.Should().Contain("abed");
            foundWords.Should().Contain("aero");
            foundWords.Should().Contain("aery");
            foundWords.Should().Contain("bad");
            foundWords.Should().Contain("bade");
            foundWords.Should().Contain("bead");
            foundWords.Should().Contain("bed");
            foundWords.Should().Contain("boa");
            foundWords.Should().Contain("bore");
            foundWords.Should().Contain("bored");
            foundWords.Should().Contain("box");
            foundWords.Should().Contain("boy");
            foundWords.Should().Contain("bread");
            foundWords.Should().Contain("bred");
            foundWords.Should().Contain("bro");
            foundWords.Should().Contain("broad");
            foundWords.Should().Contain("byre");
            foundWords.Should().Contain("dab");
            foundWords.Should().Contain("derby");
            foundWords.Should().Contain("orb");
            foundWords.Should().Contain("orbed");
            foundWords.Should().Contain("ore");
            foundWords.Should().Contain("read");
            foundWords.Should().Contain("red");
            foundWords.Should().Contain("road");
            foundWords.Should().Contain("rob");
            foundWords.Should().Contain("robe");
            foundWords.Should().Contain("robed");
            foundWords.Should().Contain("verb");
            foundWords.Should().Contain("very");
            foundWords.Should().Contain("yore");

            foundWords.Should().NotContain("board");
            foundWords.Should().NotContain("dove");
            foundWords.Should().NotContain("robbed");
            foundWords.Should().NotContain("robber");
        }
    }
}
