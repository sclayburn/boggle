using BoggleShared;
using FluentAssertions;
using Xunit;

namespace BoggleTests
{
    public class UnitTestTrie
    {
        /// <summary>
        /// Tests the Trie.Insert method to ensure that the trie is being populated correctly.
        /// </summary>
        [Fact]
        public void TestInsertNode()
        {
            TrieNode root = new TrieNode();
            Trie.Insert(root, "Apple");
            Trie.Insert(root, "Ape");
            Trie.Insert(root, "Zebra");
            Trie.Insert(root, "Baker");

            root.Children[0].Should().NotBeNull();
            root.Children[0].Children['p' - 'a'].Should().NotBeNull();
            root.Children[0].Children['p' - 'a'].Children['p' - 'a'].Should().NotBeNull();
            root.Children[0].Children['p' - 'a'].Children['p' - 'a'].Children['l' - 'a'].Should().NotBeNull();
            root.Children[0].Children['p' - 'a'].Children['p' - 'a'].Children['l' - 'a'].Children['e' - 'a'].Should().NotBeNull();
            root.Children[0].Children['p' - 'a'].Children['p' - 'a'].Children['l' - 'a'].Children['e' - 'a'].IsLeaf.Should().BeTrue();
            root.Children[0].Children['p' - 'a'].Children['e' - 'a'].Should().NotBeNull();
            root.Children[0].Children['p' - 'a'].Children['e' - 'a'].IsLeaf.Should().BeTrue();
            root.Children[25].Should().NotBeNull();
            root.Children[1].Should().NotBeNull();
        }
    }
}
