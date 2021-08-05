using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncEquation
{
    public class ProgramEquation
    {
        public static double value;
        static void Main(string[] args)
        {
            
            int count = 10;

            AsyncCalculate(count);

            
            Console.ReadLine();
        }
        public static async void AsyncCalculate(int num)
        {
            double divide = await Task.Run(() => AsyncDivide(num));
            double divisor = await Task.Run(() => AsyncDivisor(num));

            Equation(divide, divisor);
            
        }
        public static void Equation(double divide, double divisor)
        {
            value = divide / divisor;
            Console.WriteLine(value);
        }
        public static int AsyncDivide(int num)
        {
            int result = 0;
            for (int i = 1; i <= num; i++)
            {
                result += i;
            }
            return result;
        }
        public static int AsyncDivisor(int num)
        {
            int result = 0;
            for (int i = 1; i <= num; i++)
            {
                result += i * 2;
            }
            return result;
        }
        

    }
}
