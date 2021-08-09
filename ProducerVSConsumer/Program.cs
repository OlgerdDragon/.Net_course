using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ProducerVSConsumer
{
    class Program
    {

        static ProducerConsumer queue;

        static void Main()
        {
            Start();
        }
        static void Start()
        {
            queue = new ProducerConsumer();
            new Thread(new ThreadStart(ConsumerJob)).Start();

            Random rng = new Random(0);
            while (true)
            {
                
                queue.Produce();
                Thread.Sleep(1000 * rng.Next(1,3));
            }
        }

        static void ConsumerJob()
        {
            Random rng = new Random(1);
            while (true)
            {
                object o = queue.Consume();
                Console.WriteLine("\t\t\t\tConsuming {0}", o);
            }
        }
    }

    public class ProducerConsumer
    {
        readonly object listLock = new object();
        Queue queue = new Queue();
        Random rnd = new Random();
        public void Produce()
        {
            lock (listLock)
            {
                while (queue.Count == 1)
                {
                    Monitor.Wait(listLock);
                }
                int num = rnd.Next(1, 100);
                Thread.Sleep(1000 * rnd.Next(3, 9));
                queue.Enqueue(num);
                Console.WriteLine("Producing " + num);
                Monitor.Pulse(listLock);
            }
        }

        public object Consume()
        {
            lock (listLock)
            {
                Thread.Sleep(1000 * rnd.Next(3, 9));
                while (queue.Count == 0)
                {
                    Monitor.Wait(listLock);
                }
                return queue.Dequeue();
            }
        }
    }
}
    

