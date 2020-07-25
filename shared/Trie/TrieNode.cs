﻿using System;
using System.Collections.Generic;
using System.Text;

namespace boggleShared
{
    public class TrieNode
    {
        public bool IsLeaf { get; set; }
        public TrieNode[] Children { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public TrieNode()
        {
            Children = new TrieNode[Consts.c_alphabetSize];
        }
    }
}