using System;

namespace _1ThreadsDemos
{
    using System.Threading;
    using System.Threading.Tasks;

    public class Program
    {
        public static void Main(string[] args)
        {
            Demo05.Run();
        }
    }

    public class Demo05
    {
        public static void Run()
        {
            Task task1 = Task.Factory.StartNew(() =>
            {
                //operation - 10s
                Console.WriteLine("task 1");
            });

            Task task2 = task1.ContinueWith(ant =>
            {
                Console.WriteLine("task 2");
            });


            task1.Wait();
        }
    }

    public class Demo01
    {
        public static void Run()
        {
            Console.WriteLine($"[T_ID: {Thread.CurrentThread.ManagedThreadId}] Start!");

            //initialize a thread class object 
            //And pass your custom method name to the constructor parameter
            Thread t = new Thread(SomeMethod);
            t.IsBackground = true;

            var i = 25;

            //start running your thread
            t.Start();

            //while thread is running in parallel 
            //you can carry out other operations here        
            Console.WriteLine($"[T_ID: {Thread.CurrentThread.ManagedThreadId}] Press Enter to terminate!");
        }

        // respect the delegate signature:
        // public delegate void ThreadStart()  
        private static void SomeMethod()
        {
            //your code here that you want to run parallel
            //most of the time it will be a CPU bound operation

            Thread.Sleep(TimeSpan.FromSeconds(5));

            Console.WriteLine($"[T_ID: {Thread.CurrentThread.ManagedThreadId}] Hello World!");
        }
    }

    public class Demo02
    {
        private static long sum;
        private static object _lock = new object();

        public static void Run()
        {
            var start = DateTime.UtcNow;

            //n operatii - n locks

            //n operatii - nr_threads locks

            //create thread t1 using anonymous method
            Thread t1 = new Thread(() =>
            {
                long agg = 0;
                for (int i = 0; i < 100000000; i++)
                {
                    agg += i;
                }

                lock (_lock)
                {
                    //increment sum value
                    sum += agg;
                }
            });

            //create thread t2 using anonymous method
            Thread t2 = new Thread(() =>
            {
                long agg = 0;
                for (int i = 100000000; i < 200000000; i++)
                {
                    agg += i;
                }

                lock (_lock)
                {
                    //increment sum value
                    sum += agg;
                }
            });


            //start thread t1 and t2
            t1.Start();
            t2.Start();

            //wait for thread t1 and t2 to finish their execution
            t1.Join();
            t2.Join();

            var end = DateTime.UtcNow;

            //write final sum on screen
            Console.WriteLine("sum: " + sum);
            var timeSpan = end - start;
            Console.WriteLine($"duration: {timeSpan.Milliseconds} ms.");
            //11449538253042365
            //11838118256981163

            //19999999900000000

            //lock
            //19999999900000000 - 766
            Console.WriteLine("Press enter to terminate!");
            Console.ReadLine();
        }
    }

    public class Demo03
    {
        public static void Run()
        {
            // Naming a Mutex makes it available computer-wide. Use a name that's
            // unique to your company and application (e.g., include your URL).

            using (var mutex = new Mutex(false, "_Demo02Mutex"))
            {
                // Wait a few seconds if contended, in case another instance
                // of the program is still in the process of shutting down.

                Console.WriteLine("Check Mutex");

                if (!mutex.WaitOne(TimeSpan.FromSeconds(3), false))
                {
                    Console.WriteLine("Already running. Bye!");
                    return;
                }

                RunProgram();
            }
        }

        static void RunProgram()
        {
            Console.WriteLine("Running. Press Enter to exit");

            Console.ReadLine();
        }
    }

    public class Demo04
    {
        static SemaphoreSlim _sem = new SemaphoreSlim(3);

        public static void Run()
        {
            for (int i = 1; i <= 5; i++)
            {
                new Thread(Enter).Start(i);
            }
        }

        static void Enter(object id)
        {
            Console.WriteLine(id + " wants to enter");

            _sem.Wait();

            Console.WriteLine(id + " is in!");

            Thread.Sleep(2000);

            Console.WriteLine(id + " is leaving");

            _sem.Release();
        }
    }
}