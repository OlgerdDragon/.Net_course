using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncEquation
{
    class Program
    {
        static List<double> mass = new List<double>();

        static void Main(string[] args)
        {
            List<Task> taskList = new List<Task>();
            
            for (int i = 1; i <= 10; i++)
            {
                taskList.Add( Task.Run(() => AsyncCalculate(i)) );
            }
            Task.WaitAll(taskList.ToArray());


            //Thread.Sleep(10000);
        }
        static async void AsyncCalculate(int num)
        {
            int divide = await Task.Run(() => AsyncDivide(num));
            int divisor = await Task.Run(() => AsyncDivisor(num));

            Equation(divide, divisor);
        }
        static void Equation(double divide, double divisor)
        {
            double y = divide / divisor;
            mass.Add(y);
            Console.WriteLine(y);
        }
        static int AsyncDivide(int num)
        {
            int result = 0;
            for (int i = 1; i <= num; i++)
            {
                result += i;
            }
            return result;
        }
        static int AsyncDivisor(int num)
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
