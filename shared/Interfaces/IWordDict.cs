using BoggleShared;
using System.Threading.Tasks;

namespace Boggle.Game
{
    public interface IWordDict
    {
        public Task LoadDictionaryFromDiskAsync();
        public TrieNode GetWordDictionary();
    }
}
