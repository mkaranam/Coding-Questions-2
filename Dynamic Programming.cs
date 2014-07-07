using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynammicProgramming
{
    class Programs
    {
        int cnt;
        public int childSteps(int n)
        {
            int nminus1,nminus2,nminus3,ways;
            nminus3 = 1;
            nminus2 = 2;
            nminus1 = 4;
            ways = 0;
            for (int i = 3; i < n; i++)
            {
                ways = nminus1 + nminus2 + nminus3;
                nminus3 = nminus2;
                nminus2 = nminus1;
                nminus1 = ways;
            }
            return ways;
        }

        public int robotPath(int sX, int sY,int dX,int dY)
        {
            int numWays = 0;
            int X = Math.Abs(sX-dX);
            int Y = Math.Abs(sY-dY);
            int[,] ver = new int[X, Y];
            for (int i = 0; i < X; i++)
            {
                for (int j = 0; j < Y; j++)
                {
                    ver[i, j] = -1;
                }
            }
            numWays = RP(dX,dY,sX, sY, ver);
            for (int i = 0; i < X; i++)
            {
                for (int j = 0; j < Y; j++)
                {
                    Console.WriteLine("{0}|{1}: {2}", i, j, ver[i, j]);
                }
            }
            return numWays;
        }

        private int RP(int dX, int dY,int cX, int cY,int[,] ver)
        {
            if (cX == dX && cY == dY) return 0;
            if (cX == dX || cY == dY) return 1;

            int X = Math.Abs(cX - dX)-1;
            int Y = Math.Abs(cY - dY)-1;
            if (ver[X,Y] != -1) return ver[X,Y];
            ver[X,Y] = RP(dX,dY,cX + 1, cY, ver) + RP(dX,dY,cX, cY - 1, ver);
            return ver[X,Y];
        }

        public void magicIndex(int[] input)
        {
            bool found = false;
            MI(input, 0, input.Length-1, ref found);
        }

        private bool MI(int[] input, int low, int high,ref bool found)
        {
            if (found) return true;
            if (high < low) return false;
            if (input[low] > high) return false;
            if (input[high] < low) return false;
            if (low == high)
            {
                Console.WriteLine(low);
                if (input[low] == low)
                {
                    found = true;
                    Console.WriteLine("Found a magic index at: {0}", low);
                    return true;
                }
            }
            return MI(input, low, (low + high) / 2,ref found) || MI(input, (low + high) / 2+1, high,ref found);
        }

        public void printSubSets(int[] input)
        {
            StringBuilder sb = new StringBuilder();
            bool[] inset = new bool[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                inset[i] = false;
            }

            for (int i = 0; i <= input.Length; i++)
            {
                Console.WriteLine("Sets with size {0}: ", i);
                subsets(input, inset, sb, i,0);
            }
        }

        private void subsets(int[] input, bool[] inset, StringBuilder sb,int k,int start)
        {
            if (k == 0)
            {
                if (sb.Length == 0) Console.WriteLine("Empty set");
                Console.WriteLine(sb.ToString());
            }
            k--;
            for (int i = start; i < input.Length; i++)
            {
                if (!inset[i])
                {
                    sb.Append(input[i]);
                    inset[i] = true;
                    subsets(input, inset, sb, k,i);
                    sb.Remove(sb.Length-1, 1);
                    inset[i] = false;
                }
            }
        }

        public void permutStrings(String str)
        {
            char[] input = str.Trim().ToCharArray();
            str = str.Trim();
            bool[] inpermut = new bool[input.Length];
            for (int i = 0; i < input.Length; i++) inpermut[i] = false;
            StringBuilder sb = new StringBuilder();
            pStrings(input, inpermut, sb, input.Length);
        }

        private void pStrings(char[] input,bool[] inpermut,StringBuilder sb, int k){
            if (k == 0)
            {
                Console.WriteLine(sb.ToString());
            }
            k--;
            for (int i = 0; i < input.Length; i++)
            {
                if (!inpermut[i])
                {
                    inpermut[i] = true;
                    sb.Append(input[i]);
                    pStrings(input, inpermut, sb, k);
                    sb.Remove(sb.Length - 1, 1);
                    inpermut[i] = false;
                }
            }
        }

        public void permutParanthesis(int n)
        {
            StringBuilder sb = new StringBuilder();
            pParan(sb, (2 * n), n, n);
        }

        private void pParan(StringBuilder sb, int k,int nOpen,int nClose)
        {
            if (k == 0)
            {
                cnt++;
                Console.WriteLine(sb.ToString() +"|" + cnt);
            }
            //Add open paranthesis
            if (nOpen > 0)
            {
                k--;
                sb.Append("(");
                pParan(sb, k, nOpen - 1, nClose);
                sb.Remove(sb.Length - 1, 1);
                k++;
            }
            if (nClose > 0 && nOpen < nClose)
            {
                //Add close paranthesis
                k--;
                sb.Append(")");
                pParan(sb, k, nOpen, nClose - 1);
                sb.Remove(sb.Length - 1, 1);
                k++;
            }
        }

        public int makeChange(int n, int denom)
        {
            int next_demon = 0;
            switch (denom)
            {
                case 25:
                    next_demon = 10;
                    break;
                case 10:
                    next_demon = 5;
                    break;
                case 5:
                    next_demon = 1;
                    break;
                case 1:
                    return 1;
            }
            int ways = 0;
            for (int i = 0; i * denom <= n; i++)
            {
                ways += makeChange(n - i * denom, next_demon);
            }
            //Console.WriteLine(n + "|" + denom + "|" + ways);
            return ways;
        }

        public void chessBoard()
        {
            bool[,] board = new bool[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    board[i, j] = false;
                }
            }
            Stack<String> points = new Stack<string>();
            bool found = false;
            CB(0, points, board, ref found);
        }

        private void CB(int k,Stack<String> points,bool[,] board,ref bool found)
        {
            if (found) return;
            if(k == 8)
            {
                found = true;
                foreach(String s in points) Console.WriteLine(s);
                return;
            }
            k++;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (canAddtoBoard(i,j,board))
                    {
                        board[i, j] = true;
                        //Console.WriteLine("Added: {0},{1}", i, j);
                        points.Push(i + "," + j);
                        CB(k, points, board, ref found);
                        if (found) return;
                        board[i, j] = false;
                        //Console.WriteLine("Removed: {0},{1}", i, j);
                        points.Pop();
                    }
                }
            }
        }

        private bool canAddtoBoard(int i, int j, bool[,] board){


            if (board[i, j]) return false;
            for (int k = 0; k < 8; k++) if(board[i, k]) return false;
            for (int k = 0; k < 8; k++) if(board[k, j]) return false;
            int x = i;
            int y = j;
            while (x >= 0 && x <= 7 && y >= 0 && y <= 7) if(board[x++, y--]) return false;
            x = i;
            y = j;
            while (x >= 0 && x <= 7 && y >= 0 && y <= 7) if(board[x--, y++]) return false;
            x = i;
            y = j;
            while (x >= 0 && x <= 7 && y >= 0 && y <= 7) if(board[x--, y--]) return false;
            x = i;
            y = j;
            while (x >= 0 && x <= 7 && y >= 0 && y <= 7) if(board[x++, y++]) return false;

            return true;
        }

    }
}
