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
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Producing {0}", i);
                queue.Produce(i);
                Thread.Sleep(1000 * rng.Next(3));
            }
        }

        static void ConsumerJob()
        {
            
            Random rng = new Random(1);
            
            for (int i = 0; i < 10; i++)
            {
                object o = queue.Consume();
                Console.WriteLine("\t\t\t\tConsuming {0}", o);
                Thread.Sleep(rng.Next(1000));
            }
        }
    }

    public class ProducerConsumer
    {
        readonly object listLock = new object();
        Queue queue = new Queue();

        public void Produce(object o)
        {
            lock (listLock)
            {
                queue.Enqueue(o);

                      
                Monitor.Pulse(listLock);
            }
        }

        public object Consume()
        {
            lock (listLock)
            {
                
                while (queue.Count == 0)
                {
                    
                    Monitor.Wait(listLock);
                }
                return queue.Dequeue();
            }
        }
    }
}
    

