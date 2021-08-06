using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ProducerVSConsumer
{
    class Program
    {
        static public void Main()
        {
            new Program().Run();
        }

        ProducerConsumer<string> q = new ProducerConsumer<string>();

        void Run()
        {
            var threads = new[] { new Thread(Consumer), new Thread(Consumer) };
            foreach (var t in threads)
                t.Start();

            string s;
            while ((s = Console.ReadLine()).Length != 0)
                q.Enqueue(s);

            q.Stop();

            foreach (var t in threads)
                t.Join();
        }

        void Consumer()
        {
            while (true)
            {
                string s = q.Dequeue();
                if (s == null)
                    break;
                Console.WriteLine("Processing: {0}", s);
                Thread.Sleep(2000);
                Console.WriteLine("Processed: {0}", s);
            }
        }
    }

    public class ProducerConsumer<T> where T : class
    {
        object mutex = new object();
        Queue<T> queue = new Queue<T>();
        bool isDead = false;

        public void Enqueue(T task)
        {
            if (task == null)
                throw new ArgumentNullException("task");
            lock (mutex)
            {
                if (isDead)
                    throw new InvalidOperationException("Queue already stopped");
                queue.Enqueue(task);
                Monitor.Pulse(mutex);
            }
        }

        public T Dequeue()
        {
            lock (mutex)
            {
                while (queue.Count == 0 && !isDead)
                    Monitor.Wait(mutex);

                if (queue.Count == 0)
                    return null;

                return queue.Dequeue();
            }
        }

        public void Stop()
        {
            lock (mutex)
            {
                isDead = true;
                Monitor.PulseAll(mutex);
            }
        }

        async Task ProduceBatched(ChannelWriter<string> writer, int howmuch)
        {
            Random r = new();
            int n = 0;
            while (howmuch > 0)
            {
                await Task.Delay(1000 * r.Next(1, 3));
                // в реальности, задания обычно приходят пачками, так что отправим сразу много
                await writer.WaitToWriteAsync(); // это не нужно, если объём очереди не ограничен
                                                 // случайный размер пачки, вам скорее всего эта логика не будет нужна, т. к. 
                                                 // данные будут приходить группами естественным образом
                var batchSize = Math.Min(r.Next(1, 5), howmuch);
                while (batchSize-- > 0)
                {
                    // подготовим задание
                    var v = $"batched {n}";
                    if (writer.TryWrite(v))
                    {
                        n++;
                        --howmuch;
                        if (howmuch <= 0)
                            break;
                    }
                    // подготовленное, но не отправленное задание нужно сохранить
                    // для следующей итерации
                }
            }
        }
        async Task ConsumeBatches(ChannelReader<string> reader, int number)
        {
            Random r = new();
            while (await reader.WaitToReadAsync())
            {
                // вычитываем всё, что есть в канале, синхронно
                // в этой точке данные могут быть уже уйти другому потребителю,
                // так что цикл может оказаться пустым
                while (reader.TryRead(out var v))
                    Output(number, v);

                // симулируем длительную обработку результатов клиентом
                await Task.Delay(1000 * r.Next(1, 3));
            }
        }
        async Task ProduceAll(ChannelWriter<string> writer)
        {
            
            var p2 = ProduceBatched(writer, 25);
            await Task.WhenAll(p2);
            writer.Complete(); // не забываем завершить производителя!
        }

        Task ConsumeAll(ChannelReader<string> reader)
        {
            
            var c2 = ConsumeBatches(reader, 2);
            return Task.WhenAll( c2);
        }

        async Task RunAll()
        {
            Channel<string> channel = Channel.CreateUnbounded<string>();
            var p = ProduceAll(channel.Writer);
            var c = ConsumeAll(channel.Reader);
            await Task.WhenAll(p, c);
        }
        void Output(int readerNumber, string v)
        {
            // цветной вывод и прочие плюшки
            // мне лень синхронизировать вывод на консоль, хотя конечно это разделяемый ресурс
            if (Console.CursorLeft != 0)
                Console.WriteLine();
            var savedColor = Console.ForegroundColor;
            Console.ForegroundColor = (ConsoleColor)(readerNumber + 8);
            var indent = new string(' ', readerNumber * 4);
            Console.WriteLine($"{indent}[reader #{readerNumber}]: {v}");
            Console.ForegroundColor = savedColor;
        }
    }

}
