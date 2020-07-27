using BoggleShared;
using FluentAssertions;
using Xunit;

namespace BoggleTests
{
    /// <summary>
    /// Unit tests for the <see cref="Trie"/> class.
    /// </summary>
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

            var childA = root.Children[0];
            var childB = root.Children[1];
            var childZ = root.Children[25];
            int offsetP = 'p' - Consts.c_asciiCharCodeOfA;
            int offsetL = 'l' - Consts.c_asciiCharCodeOfA;
            int offsetE = 'e' - Consts.c_asciiCharCodeOfA;
            var childFirstP = childA.Children[offsetP];
            var childSecondP = childFirstP.Children[offsetP];
            var childThirdL = childSecondP.Children[offsetL];
            var childFourthE = childThirdL.Children[offsetE];
            var childThirdE = childFirstP.Children[offsetE];

            childA.Should().NotBeNull();
            childFirstP.Should().NotBeNull();
            childSecondP.Should().NotBeNull();
            childThirdL.Should().NotBeNull();
            childFourthE.Should().NotBeNull();
            childFourthE.IsLeaf.Should().BeTrue();
            childThirdE.Should().NotBeNull();
            childThirdE.IsLeaf.Should().BeTrue();
            childB.Should().NotBeNull();
            childZ.Should().NotBeNull();
        }
    }
}
