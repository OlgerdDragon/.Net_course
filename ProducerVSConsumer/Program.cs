using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ProducerVSConsumer
{
    interface IPeople
    {
        public void SetResourse(ref List<Resourse> res, int num);
    }
    interface IProducer
    {
        public void Produce();
    }
    interface IConsumer
    {
        public object Consume();
    }
    class Program
    {

        static List<Producer> producers = new List<Producer>();
        static List<Consumer> consumers = new List<Consumer>();
        static List<Resourse> resourses = new List<Resourse>();
        static void Main()
        {
            Start();
        }
        static void Start()
        {
            SetWorkZone();
            new Thread(new ThreadStart(ConsumerJob)).Start();
            Random rnd = new Random(0);
            while (true)
            {
                foreach (var producer in producers)
                {
                    producer.Produce();
                    Thread.Sleep(1000 * rnd.Next(1, 3));
                }
            }
        }
        static void SetWorkZone()
        {
            producers.Add(Factory.Producer());
            consumers.Add(Factory.Consumer());
            resourses.Add(Factory.Resourse());
            
            
            producers[0].SetResourse(ref resourses, 0);
            consumers[0].SetResourse(ref resourses, 0);

        }
        static void ConsumerJob()
        {
            while (true)
            {
                foreach (var consumer in consumers)
                {
                    object o = consumer.Consume();
                    Console.WriteLine("\t\t\t\tConsuming {0}", o);
                }
            }
        }
    }
    public class Resourse
    {
        public object listLock = new object();
        public Queue queue = new Queue();
    }
    
    public class People : Resourse , IPeople
    {
        public void SetResourse(ref List<Resourse> res, int num)
        {
            listLock = res[num].listLock;
            queue = res[num].queue;
        }
    }
    public class Producer : People , IProducer
    {
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
    }
    public class Consumer : People , IConsumer
    {
        Random rnd = new Random();
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
    class Factory
    {
        static public Resourse Resourse()
        {
            return new Resourse();
        }
        static public Producer Producer()
        {
            return new Producer();
        }
        static public Consumer Consumer()
        {
            return new Consumer();
        }
        
    }

}
    

