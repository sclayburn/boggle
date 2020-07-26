using Boggle.Game;
using BoggleShared;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Boggle.Services
{
    public class SinglethreadedGameSolverService : IGameSolver
    {
        private HashSet<string> m_foundWords;
        private int m_sideLength;
        private long m_totalOperations;

        /// <summary>
        /// Solves the boggle board using a single thread.
        /// </summary>
        /// <param name="dict">An <see cref="IWordDict"/> object that contains the dictionary to use when solving the board.</param>
        /// <param name="board">A Board that is already populated with character spaces.</param>
        public IEnumerable<string> Solve(IWordDict dict, Board board)
        {
            m_foundWords = new HashSet<string>();
            m_sideLength = board.SideLength;
            var chars = board.Chars;
            var rootNode = dict.GetWordDictionary();
            var nodeArray = rootNode.Children;
            BitArray visited = new BitArray(m_sideLength * m_sideLength);

            for (int i = 0; i < m_sideLength; i++)
            {
                for (int j = 0; j < m_sideLength; j++)
                {
                    int index = (i * m_sideLength) + j;
                    char chr = chars[index];
                    int alphaIndex = chr - Consts.c_asciiCharCodeOfA;
                    var currentNode = nodeArray[alphaIndex];

                    // Early out if we don't have any words that start with this character
                    if (currentNode == null)
                    {
                        continue;
                    }

                    string str = chr.ToString();

                    visited.SetAll(false);

                    RecursiveSearch(currentNode, chars, i, j, visited, str);
                }
            }

            return GetFoundWords();
        }

        /// <summary>
        /// Gets the total number of iteration operations that were performed solving this board.
        /// </summary>
        /// <returns>A long with the total number of operations.</returns>
        public long GetTotalOperations()
        {
            return m_totalOperations;
        }

        private IEnumerable<string> GetFoundWords()
        {
            // Not horribly performant but only called once on exit to alphabetically order the found words
            return m_foundWords.OrderBy(x => x);
        }

        private void RecursiveSearch(TrieNode node, char[] chars, int i, int j, BitArray visited, string wordBuilder)
        {
            m_totalOperations++;

            // Ensure we are on a valid space
            if (!IsValidSpace(i, j))
            {
                return;
            }

            // If we found a word, add it
            if (node.IsLeaf)
            {
                if (!m_foundWords.Contains(wordBuilder))
                {
                    m_foundWords.Add(wordBuilder);
                }
            }

            int index = (i * m_sideLength) + j;

            // Ensure we haven't visited this before
            if (visited[index])
            {
                return;
            }
            visited[index] = true;

            for (int k = i - 1; k <= i + 1; k++)
            {
                for (int l = j - 1; l <= j + 1; l++)
                {
                    int localIndex = k * m_sideLength + l;
                    if (!IsValidSpace(k, l))
                    {
                        continue;
                    }
                    if (localIndex == index)
                    {
                        continue;
                    }

                    // Check to see if the char on the board exists in this node
                    char chr = chars[localIndex];

                    int nodeIndex = chr - Consts.c_asciiCharCodeOfA;
                    var innerNode = node.Children[nodeIndex];
                    if (innerNode == null)
                    {
                        continue;
                    }
                    string nextWord = wordBuilder + chr;
                    RecursiveSearch(innerNode, chars, k, l, visited, nextWord);
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
