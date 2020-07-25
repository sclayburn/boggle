using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using boggleApp.Utils;
using boggleShared;
using Serilog;
using System.Linq;

namespace boggleApp.Game
{
    public class WordDictSinglethreaded : IWordDict
    {
        private TrieNode m_MainDictTrie;

        /// <summary>
        /// Returns the Word Dictionary trie
        /// </summary>
        /// <returns>Root TrieNode of the prefix search tree that is storing the word dictionary.  Will be null until LoadDictionaryFromDisk is called.</returns>
        public TrieNode GetWordDictionary()
        {
            return m_MainDictTrie;
        }

        /// <summary>
        /// Loads the word dictionary from disk using synchronous calls in a single threaded architecture
        /// </summary>
        /// <returns>async Task for flow control.  Async is used for conformity purposes.</returns>
        public async Task LoadDictionaryFromDisk()
        {
            m_MainDictTrie = new TrieNode();

            //Ensure that this runs on a single background thread
            await Task.Run(() => 
            {
                Log.Information(Utils.Consts.c_logWordDictSinglethreadedLoad);

                string[] lines = File.ReadAllLines(PathHelper.GetWordDictionaryPath());
                foreach (string line in lines)
                {
                    Trie.Insert(m_MainDictTrie, line);
                }

                Log.Information(Utils.Consts.c_logWordDictLoadComplete);
                Log.Information(Utils.Consts.c_logWordDictLoaded, new object[] { lines.Count() });
            });
        }
    }
}
