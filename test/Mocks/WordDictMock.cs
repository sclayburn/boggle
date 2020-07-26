using BoggleShared;
using System.Threading.Tasks;

namespace Boggle.Game
{
    public class WordDictMock : IWordDict
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
        /// Populates the dictionary from a hard coded list of words.
        /// </summary>
        /// <returns>async Task for flow control.  Async is used for conformity purposes.</returns>
        public async Task LoadDictionaryFromDiskAsync()
        {
            m_MainDictTrie = new TrieNode();

            await Task.Run(() =>
            {
                Trie.Insert(m_MainDictTrie, "abed");
                Trie.Insert(m_MainDictTrie, "aero");
                Trie.Insert(m_MainDictTrie, "aery");
                Trie.Insert(m_MainDictTrie, "bad");
                Trie.Insert(m_MainDictTrie, "bade");
                Trie.Insert(m_MainDictTrie, "bead");
                Trie.Insert(m_MainDictTrie, "bed");
                Trie.Insert(m_MainDictTrie, "boa");
                Trie.Insert(m_MainDictTrie, "bore");
                Trie.Insert(m_MainDictTrie, "bored");
                Trie.Insert(m_MainDictTrie, "box");
                Trie.Insert(m_MainDictTrie, "boy");
                Trie.Insert(m_MainDictTrie, "bread");
                Trie.Insert(m_MainDictTrie, "bred");
                Trie.Insert(m_MainDictTrie, "bro");
                Trie.Insert(m_MainDictTrie, "broad");
                Trie.Insert(m_MainDictTrie, "byre");
                Trie.Insert(m_MainDictTrie, "dab");
                Trie.Insert(m_MainDictTrie, "derby");
                Trie.Insert(m_MainDictTrie, "orb");
                Trie.Insert(m_MainDictTrie, "orbed");
                Trie.Insert(m_MainDictTrie, "ore");
                Trie.Insert(m_MainDictTrie, "read");
                Trie.Insert(m_MainDictTrie, "red");
                Trie.Insert(m_MainDictTrie, "road");
                Trie.Insert(m_MainDictTrie, "rob");
                Trie.Insert(m_MainDictTrie, "robe");
                Trie.Insert(m_MainDictTrie, "robed");
                Trie.Insert(m_MainDictTrie, "verb");
                Trie.Insert(m_MainDictTrie, "very");
                Trie.Insert(m_MainDictTrie, "yore");

                Trie.Insert(m_MainDictTrie, "board");
                Trie.Insert(m_MainDictTrie, "dove");
                Trie.Insert(m_MainDictTrie, "robbed");
                Trie.Insert(m_MainDictTrie, "robber");
            });
        }
    }
}
