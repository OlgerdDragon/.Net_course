using System;

namespace ConsoleTest
{
    class Program
    {
        static bool ChekingSimple(int n)
        {
            bool status = false;
            int level = LevelCalc(n);
            int chek = 0;

            if (n % 2 == 1) level--;
            for (int i = 0; i < level >> 1; i++)
            {
                int left = n >> (level - i);
                int right = n >> i;
                if (left % 2 == right % 2) chek++;
            }
            if (chek == level >> 1) status = true;



            return status;
        }
        
        static bool ChekingRecurse(int n, bool security=true)
        {
            bool status = false;
            int level = LevelCalc(n);

            if (n % 2 == 1 && security)
            {
                level--;
            }
            security = (n >> level - 1)%2 != 0;

            int left = n >> level;
            if (level != 0)
            {
                if (left % 2 == n % 2)
                {
                    left <<= level;
                    n -= left;
                    n >>= 1;

                    status = ChekingRecurse(n,security);
                }
            }
            else status = true;
            
            return status;

        }
        static int LevelCalc(int n)
        {
            int level = 0;
            while (n != 0)
            {
                level++;
                n >>= 1;
            }
            return level;
        }


        static void Main(string[] args)
        {
            
                int n = 4;
                Console.WriteLine(ChekingSimple(n));
                Console.WriteLine(ChekingRecurse(n));
            
            
        }
    }
}
