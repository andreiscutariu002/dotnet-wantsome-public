namespace Threads
{
    using System;
    using System.Threading;

    public class SemaphoreEx
    {
        static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(3);

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

            semaphoreSlim.Wait();

            Console.WriteLine(id + " is in!");

            Thread.Sleep(2000);

            Console.WriteLine(id + " is leaving");

            semaphoreSlim.Release();
        }
    }
}
