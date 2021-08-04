using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncEquation
{
    class Program
    {
        static List<double> mass = new List<double>();
        static List<Task> taskList = new List<Task>();
        static void Main(string[] args)
        {
            
            int count = 10;

            CalculateEguation(count);

            //Thread.Sleep(10000);
        }
        static void CalculateEguation(int count)
        {
            for (int i = 1; i <= count; i++)
            {
                taskList.Add(Task.Run(() => AsyncCalculate(i)));
            }
            Task.WaitAll(taskList.ToArray());
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
