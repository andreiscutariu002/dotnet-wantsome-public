namespace _01ThreadingDemos
{
    using System;
    using System.Threading;

    public class Demo06
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

            Thread.Sleep(4000);

            Console.WriteLine(id + " is leaving");

            _sem.Release();
        }
    }
}