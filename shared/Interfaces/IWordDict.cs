using BoggleShared;
using System.Threading.Tasks;

namespace Boggle.Game
{
    /// <summary>
    /// Interface to request the loading from disk and to request the root node of a given dictionary.
    /// </summary>
    public interface IWordDict
    {
        public Task LoadDictionaryFromDiskAsync();
        public TrieNode GetWordDictionary();
    }
}
