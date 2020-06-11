namespace Threads
{
    using System;
    using System.Threading;

    public static class StopThread
    {
        private static volatile bool cancel;

        public static void Run()
        {
            Thread t = new Thread(Print);
            t.Start("Hello");

            Thread.Sleep(5000);

            cancel = true;

            Console.Read();
        }

        private static void Print(object s)
        {
            while (!cancel)
            {
                var say = s as string;
                Console.WriteLine(say);
                Thread.Sleep(TimeSpan.FromMilliseconds(500));
            }
        }
    }
}
