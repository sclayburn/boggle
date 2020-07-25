﻿using boggleApp.Game;
using boggleShared;
using Microsoft.CSharp.RuntimeBinder;
using Serilog;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace boggleApp.Services
{
    public class AsyncGameSolverService : IGameSolver
    {
        private List<string>[] m_foundWords;
        private int m_sideLength;
        private long m_totalOperations;

        /// <summary>
        /// Solves the boggle board using the builtin C# async/task system
        /// </summary>
        /// <param name="dict">An IWordDict object that contains the dictionary to use when solving the board</param>
        /// <param name="board">A Board that is already populated with character spaces</param>
        public List<string> Solve(IWordDict dict, Board board)
        {
            m_sideLength = board.SideLength;
            int totalBoardSpaces = m_sideLength * m_sideLength;
            m_foundWords = new List<string>[totalBoardSpaces];
            var chars = board.Chars;
            var rootNode = dict.GetWordDictionary();
            var nodeArray = rootNode.Children;
            Task[] runningTasks = new Task[totalBoardSpaces];
            var visitedQueue = new ConcurrentQueue<bool[]>();

            Log.Information(Utils.Consts.c_logAsyncCreatingTasks);

            for (int i = 0; i < m_sideLength; i++)
            {
                for (int j = 0; j < m_sideLength; j++)
                {
                    int innerI = i;
                    int innerJ = j;
                    int index = (innerI * m_sideLength) + innerJ;
                    m_foundWords[index] = new List<string>();
                    Task t = new Task(() =>
                    {
                        char chr = chars[index];
                        int alphaIndex = chr - Consts.c_asciiCharCodeOfA;
                        var currentNode = nodeArray[alphaIndex];

                        //Early out if we don't have any words that start with this character
                        if (currentNode == null)
                        {
                            return;
                        }

                        string str = chr.ToString();

                        bool[] visited;
                        if (!visitedQueue.TryDequeue(out visited))
                        {
                            visited = new bool[totalBoardSpaces];
                        }

                        RecursiveSearch(currentNode, chars, innerI, innerJ, visited, str, m_foundWords[index]);

                        Array.Clear(visited, 0, visited.Length);
                        visitedQueue.Enqueue(visited);
                    });
                    runningTasks[index] = t;
                }
            }

            Log.Information(Utils.Consts.c_logAsyncTasksCreated, new object[] { runningTasks.Length });

            foreach(var task in runningTasks)
            {
                task.Start();
            }

            Log.Information(Utils.Consts.c_logAsyncWaitingToComplete);

            Task.WaitAll(runningTasks);

            return GetFoundWords();
        }

        /// <summary>
        /// Gets the total number of iteration operations that were performed solving this board
        /// </summary>
        /// <returns>A long with the total number of operations</returns>
        public long GetTotalOperations()
        {
            return m_totalOperations;
        }

        private List<string> GetFoundWords()
        {
            HashSet<string> returnVal = new HashSet<string>();
            foreach (var list in m_foundWords)
            {
                foreach (var word in list)
                {
                    if (!returnVal.Contains(word))
                    {
                        returnVal.Add(word);
                    }
                }
            }
            //Not horribly performant but only called once on exit to alphabetically order the found words
            return returnVal.OrderBy(x => x).ToList();
        }

        private void RecursiveSearch(TrieNode node, char[] chars, int i, int j, bool[] visited, string wordBuilder, List<string> foundWords)
        {
            Interlocked.Increment(ref m_totalOperations);
            
            //Ensure we are on a valid space
            if (!IsValidSpace(i, j))
            {
                return;
            }

            //If we found a word, add it
            if (node.IsLeaf)
            {
                foundWords.Add(wordBuilder);
            }

            int index = (i * m_sideLength) + j;

            //Ensure we haven't visited this before
            if (visited[index])
            {
                return;
            }
            visited[index] =  true;

            for (int k = i - 1; k <= i + 1; k++)
            {
                for (int l = j - 1; l <= j + 1; l++)
                {
                    if (!IsValidSpace(k, l))
                    {
                        continue;
                    }
                    int localIndex = k * m_sideLength + l;
                    if (localIndex == index)
                    {
                        continue;
                    }

                    //Check to see if the char on the board exists in this node
                    char chr = chars[localIndex];

                    int nodeIndex = chr - Consts.c_asciiCharCodeOfA;
                    var innerNode = node.Children[nodeIndex];
                    if (innerNode == null)
                    {
                        continue;
                    }
                    string nextWord = wordBuilder + chr;
                    
                    RecursiveSearch(innerNode, chars, k, l, visited, nextWord, foundWords);
                }
            }

            visited[index] = false;
        }

        private bool IsValidSpace(int i, int j)
        {
            if ((i >= 0 && i < m_sideLength) && (j >= 0 && j < m_sideLength))
            {
                return true;
            }
            return false;
        }
    }
}
