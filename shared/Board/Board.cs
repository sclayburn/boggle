namespace BoggleShared
{
    /// <summary>
    /// The game board poco.
    /// </summary>
    public class Board
    {
        public int SideLength { get; set; }
        public char[] Chars { get; set; }
        public bool IsInitialized { get; set; }
    }
}
