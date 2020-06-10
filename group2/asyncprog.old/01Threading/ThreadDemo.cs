namespace _01Threading
{
    using System;
    using System.Threading;

    internal class ThreadDemo
    {
        // The ThreadProc method is called when the thread starts.
        // It loops ten times, writing to the console and yielding 
        // the rest of its time slice each time, and then ends.
        public static void ThreadProc()
        {
            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine("ThreadProc: {0}", i);

                // Yield the rest of the time slice.
                Thread.Sleep(TimeSpan.FromMilliseconds(100));
            }
        }

        public static void Run()
        {
            Console.WriteLine("Main thread: Start a second thread.");
            // The constructor for the Thread class requires a ThreadStart 
            // delegate that represents the method to be executed on the 
            // thread.  C# simplifies the creation of this delegate.
            var t = new Thread(ThreadProc);

            // Start ThreadProc.  Note that on a uniprocessor, the new 
            // thread does not get any processor time until the main thread 
            // is preempted or yields.  Uncomment the Thread.Sleep that 
            // follows t.Start() to see the difference.
            t.Start();
            //Thread.Sleep(0);

            for (var i = 0; i < 4; i++)
            {
                Console.WriteLine("Main thread: Do some work.");

                Thread.Sleep(TimeSpan.FromMilliseconds(100));
            }

            Console.WriteLine("Main thread: Call Join(), to wait until ThreadProc ends.");

            t.Join();

            Console.WriteLine("Main thread: ThreadProc.Join has returned. ");

            for (var i = 0; i < 4; i++)
            {
                Console.WriteLine("Main thread: Do some work after ThreadProc finished.");

                Thread.Sleep(TimeSpan.FromMilliseconds(100));
            }

            Console.WriteLine("Finished. Press Enter to end program.");

            Console.ReadLine();
        }
    }
}
