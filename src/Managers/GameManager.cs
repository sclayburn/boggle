using Boggle.Game;
using Boggle.Options;
using Boggle.Services;
using Boggle.Validation;
using BoggleShared;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boggle.Managers
{
    /// <summary>
    /// Is the translation interface between the CommandLineParser assembly and the actual board solver logic.
    /// </summary>
    public static class GameManager
    {
        private static IWordDict m_wordDict;
        private static CmdLineOptions m_cmdLineOpts;

        /// <summary>
        /// Manager that will handle running the game cmdline options and return a status code to the caller.
        /// </summary>
        /// <param name="opts">CmdLineOptions object that contains the parsed command line args.</param>
        /// <returns>Error Code.  0 = Success, 1 = Failure.</returns>
        public static async Task<int> RunAndReturnExitCodeAsync(CmdLineOptions opts)
        {
            if (opts == null)
            {
                throw new ArgumentNullException(nameof(opts));
            }

            RuntimeTimer.RegisterAppStart();

            m_cmdLineOpts = opts;

            // Validate Input
            ValidateCmdLine val = new ValidateCmdLine();
            val.IsCmdLineValid(opts);

            // Load the dictionary from disk
            await LoadDictFromDiskAsync();

            await Task.Run(() =>
            {
                BoardMaker maker = new BoardMaker();
                maker.LoadRandomBoard(opts.SideLength);
                Board board = maker.GetBoard();

                IGameSolver solver = null;
                if (m_cmdLineOpts.Singlethreaded)
                {
                    solver = new SinglethreadedGameSolverService();
                }
                else
                {
                    solver = new AsyncGameSolverService();
                }
                var foundWords = solver.Solve(m_wordDict, board);

                // This is a little odd.  I want to stop timing before I write all the words, which takes a long time
                RuntimeTimer.RegisterAppEnd();

                var totalOperations = solver.GetTotalOperations();
                PrintFoundWordsToConsole(foundWords, totalOperations, opts.ForceWriteFoundWords);
            });



            return Utils.Consts.c_exitCodeSuccess;
        }

        private static void PrintFoundWordsToConsole(IEnumerable<string> foundWords, long totalOperations, bool forceWriteFoundWords)
        {
            int wordLength = foundWords.Count();
            if (wordLength < 64 || forceWriteFoundWords)
            {
                foreach (string word in foundWords)
                {
                    Console.WriteLine(word);
                }
                Console.WriteLine();
            }
            double time = RuntimeTimer.CalcAppRuntimeUs() / (double)totalOperations;
            Log.Information(Utils.Consts.c_logTimingInfo, wordLength, totalOperations, Math.Round(time * 1000, 2));
        }

        private static async Task LoadDictFromDiskAsync()
        {
            if (m_cmdLineOpts.Singlethreaded)
            {
                m_wordDict = new WordDictSinglethreaded();
            }
            else
            {
                m_wordDict = new WordDictAsync();
            }

            await m_wordDict.LoadDictionaryFromDiskAsync();
        }
    }
}
