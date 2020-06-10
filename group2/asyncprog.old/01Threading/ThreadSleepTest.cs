namespace _01Threading
{
    using System;
    using System.Threading;

    public class ThreadSleepTest
    {
        public static void Run()
        {
            var t = new Thread(() =>
            {
                for (int i = 0; i < 1000; i++)
                {
                    try
                    {
                        Console.WriteLine($"step {i}");
                        Thread.Sleep(TimeSpan.FromSeconds(10));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.GetType().FullName);
                    }
                }
            });

            t.IsBackground = false;
            t.Start();

            Thread.Sleep(TimeSpan.FromSeconds(2));

            t.Interrupt();

            Thread.Sleep(TimeSpan.FromSeconds(2));

            t.Interrupt();

            Thread.Sleep(TimeSpan.FromSeconds(2));

            t.Abort(); // not supported in .net core
        }
    }
}