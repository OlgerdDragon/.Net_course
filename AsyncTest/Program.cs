using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsyncTest
{
    class Program
    {
        
        static void Main(string[] args)
        {
            FactorialAsync();


        }
        static void FactorialAsync()
        {
            Task t1 = Task.Run(() => AsyncWriter(1));
            Task t2 = Task.Run(() => AsyncWriter(2));
            Task t3 = Task.Run(() => AsyncWriter(3));
            List<Task> tasks = new List<Task>();
            tasks.Add(t1);
            tasks.Add(t2);
            tasks.Add(t3);
            Task.WaitAll(tasks.ToArray());
        }
        static void AsyncWriter(int par)
        {
            for (int i = 0; i < 999; i++)
            {
                Console.Write(par);
            }
        }
    }
}
