using System;

namespace MinWithMax
{
    public class MinWithMax
    {

        public static int Serch(int[][] mas)
        {
            try 
            { 
            int[] max = new int[mas.Length];

            for (int i = 0; i < mas.Length; i++)
            {
                max[i] = mas[i][0];
                for (int j = 0; j < mas[i].Length; j++)
                {
                    if (max[i] < mas[i][j])
                        max[i] = mas[i][j];
                }

            }
            int min = max[0];
            for (int i = 1; i < max.Length; i++)
            {
                if (min > max[i])
                    min = max[i];
            }

            return min; 

            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
        static void Main(string[] args)
        {
            Random rnd = new Random();
            
            int[][] mas = new int[rnd.Next(1, 9)][];
            for (int i = 0; i < mas.Length; i++)
            {
                mas[i] = new int[rnd.Next(1, 9)];
            }

            for (int i = 0; i < mas.Length; i++)
            {
                for (int j = 0; j < mas[i].Length; j++)
                {
                    mas[i][j] = rnd.Next(0, 100);
                    Console.Write(mas[i][j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            Console.WriteLine("Min with max: " + Serch(mas));
        }
    }
}
