﻿using System;
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
    public class WordDictAsync : IWordDict
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
        /// Loads the word dictionary from disk using the Async Task system which is multithreaded
        /// </summary>
        /// <returns>async Task for flow control</returns>
        public async Task LoadDictionaryFromDisk()
        {
            int alphabetSize = boggleShared.Consts.c_alphabetSize;

            m_MainDictTrie = new TrieNode();
            LinkedList<string>[] asyncLists = new LinkedList<string>[alphabetSize];
            Task[] runningTasks = new Task[alphabetSize];


            Log.Information(Utils.Consts.c_logWordDictAsyncLoad);
            
            string[] lines = await File.ReadAllLinesAsync(PathHelper.GetWordDictionaryPath());
            foreach(string line in lines)
            {
                string word = line.ToLower();
                int index = word[0] - 'a';
                if (asyncLists[index] == null)
                {
                    asyncLists[index] = new LinkedList<string>();
                }
                asyncLists[index].AddLast(line);
            }

            TaskFactory factory = Task.Factory;
            for (int i=0; i < alphabetSize; i++)
            {
                int index = i;
                Task t = factory.StartNew(() => 
                {
                    TrieNode node = new TrieNode();
                    var list = asyncLists[index];
                    //Early out if we don't have any words that start with this letter
                    if (list == null || list.Count == 0)
                    {
                        return;
                    }
                    foreach(string word in list)
                    {
                        Trie.Insert(m_MainDictTrie, word);
                    }
                });
                runningTasks[i] = t;
            }

            Task.WaitAll(runningTasks);

            Log.Information(Utils.Consts.c_logWordDictLoadComplete);
            Log.Information(Utils.Consts.c_logWordDictLoaded, new object[] { lines.Count() });
        }
    }
}
