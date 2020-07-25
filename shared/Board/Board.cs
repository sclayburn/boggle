using System;
using System.Collections.Generic;
using System.Text;

namespace boggleShared
{
    public class Board
    {
        public int SideLength { get; set; }
        public char[] Chars { get; set; }
        public bool IsInitialized { get; set; }
    }
}
