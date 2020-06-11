namespace Tasks
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    class Program
    {
        static void Main(string[] args)
        {
            TaskContinuation04.Run();
        }
    }

    class TaskCancellation
    {
        public static void Run()
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;

            Task t = new Task(() =>
            {
                //while (!token.IsCancellationRequested)
                //{
                //    Console.WriteLine("Working ...");
                //    Thread.Sleep(1000);
                //}

                while (true)
                {
                    Console.WriteLine("Working ...");
                    Thread.Sleep(1000);
                    token.ThrowIfCancellationRequested();
                }
            }, token);

            t.Start();

            Thread.Sleep(3000);

            tokenSource.Cancel();

            Console.WriteLine("Finish!");
        }
    }

    class TaskContinuation01
    {
        public static void Run()
        {
            var t1 = new Task<string>(() =>
            {
                // get from db
                Thread.Sleep(2000);
                Console.WriteLine("finish db");
                return "data";
            });

            t1.Start();
            t1.Wait();

            var result = t1.Result;

            var t2 = new Task(() =>
            {
                Thread.Sleep(3000);
                Console.WriteLine(result);
            });

            t2.Start();
            t2.Wait();

            Console.WriteLine("Finish!");
        }
    }

    class TaskContinuation02
    {
        public static void Run()
        {
            var t1 = new Task<string>(() =>
            {
                // get from db
                Thread.Sleep(2000);
                Console.WriteLine("finish db");
                return "data";
            });

            var t2 = t1.ContinueWith(pred =>
            {
                if (pred.Exception != null)
                {
                    //exception
                }
                else
                {
                    Thread.Sleep(3000);
                    Console.WriteLine(pred.Result);
                }
            });

            t1.Start();
            t1.Wait();
            t2.Wait();

            Console.WriteLine("Finish!");
        }
    }

    internal class TaskContinuation04
    {
        internal static void Run()
        {
            // throws exception on Wait() 
            Task<int> task1 = Task.Factory.StartNew(() =>
            {
                return 10;
            });

            task1.ContinueWith(ant =>
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ant.Exception.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }, TaskContinuationOptions.OnlyOnFaulted);

            task1.ContinueWith(ant =>
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(ant.Result);
                Console.ForegroundColor = ConsoleColor.White;
            }, TaskContinuationOptions.NotOnFaulted);

            Console.WriteLine("Press Enter to terminate!");
            Console.ReadLine();
        }
    }
}