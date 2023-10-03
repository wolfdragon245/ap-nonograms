using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BK_Picross
{
    public class Board
    {
        public static bool[,] board = new bool[10,10];
        public static bool[,] flags = new bool[10,10];
        public static bool[,] boardSolution = new bool[10,10];
        public static Random random = new Random();

        public Board()
        {
            randomizeSolution();
        }

        public static bool checkBoard()
        {
            for (int k = 0; k < board.GetLength(0); k++)
            {
                for (int i = 0; i < board.GetLength(1); i++)
                {
                    if (board[k,i] != boardSolution[k,i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static void clearBoard()
        {
            for (int k = 0; k < board.GetLength(0); k++)
            {
                for (int i = 0; i < board.GetLength(1); i++)
                {
                    board[k,i] = false;
                }
            }
        }

        public static void clearFlag()
        {
            for (int k = 0; k < flags.GetLength(0); k++)
            {
                for (int i = 0; i < flags.GetLength(1); i++)
                {
                    flags[k, i] = false;
                }
            }
        }

        public static void randomizeSolution()
        {
            for (int k = 0;k < boardSolution.GetLength(0); k++)
            {
                for (int i = 0;i < boardSolution.GetLength(1);i++)
                {
                    boardSolution[k, i] = random.Next(2) == 1;
                }
            }
        }
    }
}
