using BoggleShared;
using System.Collections.Generic;

namespace Boggle.Game
{
    public interface IGameSolver
    {
        public IEnumerable<string> Solve(IWordDict dict, Board board);
        public long GetTotalOperations();
    }
}
