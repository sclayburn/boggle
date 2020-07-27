namespace BoggleShared
{
    public class TrieNode
    {
        public bool IsLeaf { get; set; }
        public TrieNode[] Children { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public TrieNode(int initialCapacity = Consts.c_alphabetSize)
        {
            Children = new TrieNode[initialCapacity];
        }
    }
}
