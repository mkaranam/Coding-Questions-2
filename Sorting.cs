using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting
{
    class Program
    {
        public void mergeSortedArrays(int[] large, int[] small,int larLen)
        {
            if (larLen + small.Length > large.Length) return;
            int start = larLen + small.Length - 1;
            int smallCounter = small.Length -1;
            int larCounter = larLen -1;
            for (int i = start; i >= 0; i--)
            {
                if (larCounter < 0) large[i] = small[smallCounter--];
                else if (smallCounter < 0) large[i] = large[larCounter--];
                else if (small[smallCounter] > large[larCounter]) large[i] = small[smallCounter--];
                else large[i] = large[larCounter--];
            }
        }

        public void groupAnagrams(ArrayList input)
        {
            Dictionary<String, List<String>> dic = new Dictionary<string, List<String>>();
            foreach(String s in input)
            {
                String temp = sort(s);
                if(dic.ContainsKey(temp)){
                    List<String> li= dic[temp];
                    li.Add(s);
                }
                else
                {
                    List<String> li = new List<String>();
                    li.Add(s);
                    dic.Add(temp,li);
                }
            }
            input.Clear();
            foreach (KeyValuePair<String, List<String>> pair in dic)
            {
                foreach (String temp in pair.Value)
                {
                    input.Add(temp);
                }
            }
        }

        private String sort(String input)
        {
            char[] c = input.ToCharArray();
            Array.Sort(c);
            return new String(c);
        }

        public int findOnRotatedArray(int[] input,int key)
        {
            return findOnRotatedArray(input, 0, input.Length - 1, key);
        }

        private int findOnRotatedArray(int[] input, int low, int high, int key)
        {
            if (low > high) return -1;
            int middle = (low + high) / 2;
            if (key == input[middle]) return middle;
            if (input[low] < input[middle])
            {
                if (key >= input[low] && key <= input[middle - 1]) return findOnRotatedArray(input, low, middle - 1, key);    //sorted left partition
                else return findOnRotatedArray(input, middle + 1, high, key);
            }
            else if (input[middle + 1] < input[high])
            {
                if (key >= input[middle + 1] && key <= input[high]) return findOnRotatedArray(input, middle + 1, high, key);
                else return findOnRotatedArray(input, low, middle - 1, key);
            }
            else if (input[low] == input[middle])
            {
                if (input[middle] != input[high])
                {
                    return findOnRotatedArray(input, middle + 1, high, key);
                }
                else
                {
                    int temp = findOnRotatedArray(input, low, middle - 1, key);
                    if (temp == -1) temp = findOnRotatedArray(input, middle + 1, high, key);
                    return temp;                   
                }
            }
            else return -1;
        }

        public int findString(String[] input, String key)
        {
            if (input.Length == 0 || key.Length == 0) return -1;
            return findString(key, input, 0, input.Length-1);
        }

        private int findString(String key, String[] input, int low, int high)
        {
            if (low > high) return -1;
            if (low == high)
            {
                if (key == input[low]) return low;
                else return -1;
            }
            int middle;
            middle = (low + high) / 2;
            
            while(input[middle].Length == 0 && middle >= low) middle--;   //Move to the left
            if (middle < low)
            {
                middle = (low + high) / 2;
                while (input[middle].Length == 0 && middle <= high) middle++;   //Move to the left
                if (middle > high) return -1;
            }
            if (key == input[middle]) return middle;
            if (string.Compare(key, input[middle]) < 0) return findString(key, input, low, middle - 1);
            else return findString(key, input, middle + 1, high);
        }

        public int[] findMatrixElement(int[,] input, int key)
        {
            return findMatrixElement(key,input,0,input.GetLength(0)-1,0,input.GetLength(1)-1);
        }

        private int[] findMatrixElement(int key, int[,] input, int rS, int rE, int cS, int cE)
        {
            if (rS > rE || cS > cE) return new int[]{-1, -1};

            int rM, cM;
            rM = (rS + rE) / 2;
            cM = (cS + cE) / 2;
            if (key == input[rM,cM]) return new int[] { rM, cM };
            int temp = BSHelper(input, rM, cM, key, rS, rE, true);
            if(temp > -1) return new int[] { temp, cM };
            temp = BSHelper(input, rM, cM, key, cS, cE, false);
            if (temp > -1) return new int[] { rM, temp };
            
            if (rM > rS && cM > cS && key >=input[rS,cS] && key <= input[rM - 1 , cM - 1]) return findMatrixElement(key, input, rS, rM-1, cS, cM-1);  //Top Left partition
            else if (rM > rS && cM < cE && key >= input[rS, cM + 1] && key <= input[rM - 1, cE]) return findMatrixElement(key, input, rS, rM - 1, cM + 1, cE);    //Top right partition
            else if (rM < rE && cM > cS && key >= input[rM + 1, cS] && key <= input[rE, cM - 1]) return findMatrixElement(key, input, rM + 1, rE, cS, cM - 1);    //Bottom left partition
            else if (rM < rE && cM < cE && key >= input[rM + 1, cM + 1] && key <= input[rE, cE]) return findMatrixElement(key, input, rM + 1, rE, cM + 1, cE);  //Bottom right partition
            else return new int[] { -1, -1 };
        }

        private int BSHelper(int[,] input, int row, int col, int key, int low, int high, bool isRowSearch)
        {
            
            if (low > high) return -1;
            int mid = (low + high) / 2;
            if (isRowSearch)
            {
                if (input[mid,col] == key) return mid;
                if (key < input[mid, col]) return BSHelper(input, row, col, key, low, mid - 1, isRowSearch);
                else return BSHelper(input, row, col, key, mid + 1, high,isRowSearch);
            }
            else
            {
                if (input[row, mid] == key) return mid;
                if (key < input[row, mid]) return BSHelper(input, row, col, key, low, mid - 1, isRowSearch);
                else return BSHelper(input, row, col, key, mid + 1, high, isRowSearch);
            }
            
        }


    }
}
