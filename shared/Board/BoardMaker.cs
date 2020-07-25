using System;
using System.Security.Cryptography;

namespace boggleShared
{
    public class BoardMaker
    {
        private Board m_board;
        
        /// <summary>
        /// Constructor
        /// </summary>
        public BoardMaker()
        {
            m_board = new Board();
        }

        /// <summary>
        /// Will generate a new random square boggle board that is sideLength squared large
        /// </summary>
        /// <param name="sideLength">Length of a single side.  8 would equal an 8x8 board.</param>
        public void LoadRandomBoard(int sideLength)
        {
            if (sideLength < 1 || sideLength > 1024)
            {
                throw new ArgumentOutOfRangeException(Consts.c_paramNameSideLength, Consts.c_argExceptionDescSideLength);
            }

            
            m_board.SideLength = sideLength;
            char[] chars = new char[sideLength * sideLength];
            for (int x = 0; x < sideLength; x++)
            {
                for(int y = 0; y < sideLength; y++)
                {
                    int index = (x * sideLength) + y;
                    chars[index] = GetRandomChar();
                }
            }

            m_board.Chars = chars;
            m_board.IsInitialized = true;
        }

        /// <summary>
        /// Will use the given array to populate the boggle board.  Array length must by sideLength squard.  For a sideLength of 8, array must be 64 elements in length.
        /// </summary>
        /// <param name="board">Array that contains the chars to use for the board</param>
        /// <param name="sideLength">Length of a single side</param>
        public void LoadGivenBoard(char[] board, int sideLength)
        {
            if (board.Length != sideLength * sideLength)
            {
                throw new ArgumentOutOfRangeException(Consts.c_paramNameBoard, Consts.c_argExceptionDescBoard);
            }

            m_board.Chars = board;
            m_board.SideLength = sideLength;
            m_board.IsInitialized = true;
        }

        /// <summary>
        /// Get the current board
        /// </summary>
        /// <returns>Returns the current Board.  If Board has not been initialized, it will return null</returns>
        public Board GetBoard()
        {
            if (!m_board.IsInitialized)
            {
                return null;
            }
            return m_board;
        }

        private char GetRandomChar()
        {
            return (char)RandomNumberGenerator.GetInt32(Consts.c_asciiCharCodeOfA, Consts.c_asciiCharCodeOfZ + 1);
        }
    }
}
