using BoggleShared;
using System.Collections.Generic;

namespace Boggle.Game
{
    /// <summary>
    /// Interface to request a solution of a <see cref="Board"/> with a given <see cref="IWordDict"/>.
    /// </summary>
    public interface IGameSolver
    {
        public IEnumerable<string> Solve(IWordDict dict, Board board);
        public long GetTotalOperations();
    }
}
