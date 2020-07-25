using System;
using System.Collections.Generic;
using System.Text;

namespace boggleShared
{
    public static class Trie
    {
        /// <summary>
        /// Insert a new string (key) into the trie, starting at root
        /// </summary>
        /// <param name="root">TrieNode that is the root of the structure</param>
        /// <param name="key">The string key to be inserted</param>
        public static void Insert(TrieNode root, string key)
        {
            //normalize all keys to lowercase
            key = key.ToLower();
            
            int keyCount = key.Length;
            TrieNode currentNode = root;

            for (int i=0; i < keyCount; i++)
            {
                int index = key[i] - Consts.c_firstLetterOfAlphabet;
            
                if (currentNode.Children[index] == null)
                {
                    currentNode.Children[index] = new TrieNode();
                }

                currentNode = currentNode.Children[index];
            }

            //Last node should be marked as a leaf node
            currentNode.IsLeaf = true;
        }
    }
}
