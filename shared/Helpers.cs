﻿using System;

namespace BoggleShared
{
    /// <summary>
    /// Generic & misc helper functions.
    /// </summary>
    public static class Helpers
    {
        /// <summary>
        /// Write the layout and content of the board to the Console.  
        /// </summary>
        /// <param name="board">The Board that you want to write to the console.</param>
        public static void WriteBoardToConsole(Board board)
        {
            int sideLength = board.SideLength;
            var chars = board.Chars;

            Console.WriteLine(Consts.c_logBoardStart);
            Console.WriteLine();
            for (int i = 0; i < sideLength; i++)
            {
                for (int j = 0; j < sideLength; j++)
                {
                    int index = (i * sideLength) + j;
                    char chr = chars[index];

                    Console.Write(chr);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine(Consts.c_logBoardEnd);
        }
    }
}
