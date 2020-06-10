namespace _20ConcurrentDemos
{
    using System;
    using System.Threading;

    internal class SemaphoreSlimDemo
    {
        private static readonly SemaphoreSlim Sem = new SemaphoreSlim(3); // Capacity of 3

        public static void Run()
        {
            for (var i = 1; i <= 5; i++)
            {
                new Thread(Enter).Start(i);
            }
        }

        private static void Enter(object id)
        {
            Console.WriteLine(id + " wants to enter");

            Sem.Wait();

            Console.WriteLine(id + " is in!");

            Thread.Sleep(2000); // some operation

            Console.WriteLine(id + " is leaving");

            Sem.Release();
        }
    }
}