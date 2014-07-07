using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitManipulation
{
    class Bits
    {
        public bool getBit(int number, int i)
        {
            return (((1 << i) & number) != 0);
        }

        public int setBit(int number, int i)
        {
            return ((1 << i) | number);
        }

        public int flipBit(int number, int i)
        {
            return ((1 << i) ^ number);
        }

        public int clearBit(int number, int i)
        {
            int mask = ~(1 << i);
            return (mask & number);
        }

        public int clearMSP(int number, int i)
        {
            return (number & ((~0) >> i));
        }

        public int clearMSPfromEnd(int number, int i)
        {
            return (number & ((~0) << i));
        }

        public int insertMinN(int m, int n, int j, int i)
        {
            int mask = ((~0) << j) | ((1 << i) - 1);
            n = n & mask;
            m = m << i;
            return (n | m);
        }

        public int BitsToConvert(int m, int n)
        {
            int count = 0;
            int result = m ^ n;
            while (result > 0)
            {
                if ((result & 1) != 0) count++;
                result >>= 1;
            }
            return count;
        }

        public int swapOddEvenBits(int input)
        {
            Console.WriteLine(Convert.ToString(input, 2));
            int oddmask = 0;
            int evenmask = 1;
            for (int i = 0; i < 32; i+=2)
            {
                oddmask = (oddmask << 2)|1;
                evenmask = (evenmask << 2) | 1;
            }
            evenmask = evenmask << 1;
            int result=((input & oddmask)>>1)|((input & evenmask)<<1);
            Console.WriteLine(Convert.ToString(result, 2));
            return result;
        }

        
    }
}
