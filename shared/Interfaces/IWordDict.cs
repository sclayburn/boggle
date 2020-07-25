using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using boggleShared;
using System.Linq;

namespace boggleApp.Game
{
    public interface IWordDict
    {
        public Task LoadDictionaryFromDisk();
        public TrieNode GetWordDictionary();
    }
}
