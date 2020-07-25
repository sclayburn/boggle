using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using boggleApp.Options;
using boggleApp.Validation;
using boggleApp.Utils;
using boggleApp.Game;
using Serilog.Formatting.Json;
using boggleApp.Services;
using boggleShared;
using System.Runtime.CompilerServices;
using Serilog;

namespace boggleApp.Managers
{
    public static class GameManager
    {
        private static IWordDict m_wordDict;
        private static CmdLineOptions m_cmdLineOpts;
        
        /// <summary>
        /// Manager that will handle running the game cmdline options and return a status code to the caller
        /// </summary>
        /// <param name="opts">CmdLineOptions object that contains the parsed command line args</param>
        /// <returns>Error Code.  0 = Success, 1 = Failure</returns>
        public static async Task<int> RunAndReturnExitCode(CmdLineOptions opts)
        {
            if (opts == null)
            {
                throw new ArgumentNullException(Utils.Consts.c_paramNameOpts);
            }

            RuntimeTimer.RegisterAppStart();

            m_cmdLineOpts = opts;

            //Validate Input
            ValidateCmdLine val = new ValidateCmdLine();
            val.IsCmdLineValid(opts);

            //Load the dictionary from disk
            await LoadDictFromDisk();

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

                //This is a little odd.  I want to stop timing before I write all the words, which takes a long time
                RuntimeTimer.RegisterAppEnd();

                var totalOperations = solver.GetTotalOperations();
                PrintFoundWordsToConsole(foundWords, totalOperations);
            });



            return Utils.Consts.c_ExitCodeSuccess;
        }

        private static void PrintFoundWordsToConsole(List<string> foundWords, long totalOperations)
        {
            if (foundWords.Count < 64)
            {
                foreach (string word in foundWords)
                {
                    Console.WriteLine(word);
                }
                Console.WriteLine();
            }
            double time = RuntimeTimer.CalcAppRuntimeUs() / (double)totalOperations;
            Log.Information(Utils.Consts.c_logTimingInfo, new object[] { foundWords.Count, totalOperations, Math.Round(time * 1000, 2) });
        }

        private static async Task LoadDictFromDisk()
        {
            if (m_cmdLineOpts.Singlethreaded)
            {
                m_wordDict = new WordDictSinglethreaded();
            }
            else
            {
                m_wordDict = new WordDictAsync();
            }

            await m_wordDict.LoadDictionaryFromDisk();
        }
    }
}
