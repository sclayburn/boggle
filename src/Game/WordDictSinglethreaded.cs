using Boggle.Utils;
using BoggleShared;
using Serilog;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Boggle.Game
{
    public class WordDictSinglethreaded : IWordDict
    {
        private TrieNode m_MainDictTrie;

        /// <summary>
        /// Returns the Word Dictionary trie.
        /// </summary>
        /// <returns>Root TrieNode of the prefix search tree that is storing the word dictionary.  Will be null until LoadDictionaryFromDisk is called.</returns>
        public TrieNode GetWordDictionary()
        {
            return m_MainDictTrie;
        }

        /// <summary>
        /// Loads the word dictionary from disk using synchronous calls in a single threaded architecture.
        /// </summary>
        /// <returns>async Task for flow control.  Async is used for conformity purposes.</returns>
        public async Task LoadDictionaryFromDiskAsync()
        {
            m_MainDictTrie = new TrieNode();

            // Ensure that this runs on a single background thread
            await Task.Run(() =>
            {
                Stopwatch timer = Stopwatch.StartNew();

                Log.Information(Utils.Consts.c_logWordDictSinglethreadedLoad);

                string[] lines = File.ReadAllLines(PathHelper.GetWordDictionaryPath());
                foreach (string line in lines)
                {
                    Trie.Insert(m_MainDictTrie, line);
                }

                timer.Stop();

                Log.Information(Utils.Consts.c_logWordDictLoadComplete);
                Log.Information(Utils.Consts.c_logWordDictLoaded, lines.Length, timer.ElapsedMilliseconds);
            });
        }
    }
}
