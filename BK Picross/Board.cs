using Archipelago.MultiClient.Net.Models;
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
            bool valid = false;
            if (vertCheck() && horzCheck())
            {
                valid = true;
            }
            return valid;
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
            clearFlag();
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

        public static bool vertCheck()
        {
            int row = 0;
            bool valid = true;
            List<int> check = new List<int>();
            List<int> hint = new List<int>();
            for (int k = 0; k < board.GetLength(0); k++)
            {
                for (int i = board.GetLength(1) - 1; i >= 0; i--)
                {
                    if (board[k, i])
                    {
                        row++;
                    }
                    else if (row > 0)
                    {
                        check.Add(row);
                        row = 0;
                    }
                    if (i == 0 && board[k, i])
                    {
                        check.Add(row);
                        row = 0;
                    }
                    if (i == 0 && check.Count <= 0)
                    {
                        check.Add(0);
                    }
                }
                for (int i = boardSolution.GetLength(1) - 1; i >= 0; i--)
                {
                    if (boardSolution[k, i])
                    {
                        row++;
                    }
                    else if (row > 0)
                    {
                        hint.Add(row);
                        row = 0;
                    }
                    if (i == 0 && boardSolution[k, i])
                    {
                        hint.Add(row);
                        row = 0;
                    }
                    if (i == 0 && check.Count <= 0)
                    {
                        hint.Add(0);
                    }
                }
                for (int i = 0;i < check.Count; i++)
                {
                    if (check.Count < hint.Count || check.Count > hint.Count)
                    {
                        valid = false;
                        break;
                    }
                    if (hint[i] != check[i])
                    {
                        valid = false;
                    }
                }
                check.Clear();
                hint.Clear();
            }
            return valid;
        }

        public static bool horzCheck()
        {
            int row = 0;
            bool valid = true;
            List<int> check = new List<int>();
            List<int> hint = new List<int>();
            for (int k = board.GetLength(1) - 1; k >= 0; k--)
            {
                for (int i = board.GetLength(0) - 1; i >= 0; i--)
                {
                    if (board[i, k])
                    {
                        row++;
                    }
                    else if (row > 0)
                    {
                        check.Add(row);
                        row = 0;
                    }
                    if (i == 0 && board[i, k])
                    {
                        check.Add(row);
                        row = 0;
                    }
                    if (i == 0 && check.Count <= 0)
                    {
                        check.Add(0);
                    }
                }
                for (int i = boardSolution.GetLength(0) - 1; i >= 0; i--)
                {
                    if (boardSolution[i, k])
                    {
                        row++;
                    }
                    else if (row > 0)
                    {
                        hint.Add(row);
                        row = 0;
                    }
                    if (i == 0 && boardSolution[i, k])
                    {
                        hint.Add(row);
                        row = 0;
                    }
                    if (i == 0 && hint.Count <= 0)
                    {
                        hint.Add(0);
                    }
                }
                for (int i = 0; i < check.Count; i++)
                {
                    if (check.Count < hint.Count || check.Count > hint.Count)
                    {
                        valid = false;
                        break;
                    }
                    if (hint[i] != check[i])
                    {
                        valid = false;
                    }
                }
                check.Clear();
                hint.Clear();
            }
            return valid;
        }
    }
}
