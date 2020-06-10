namespace _05NonBlocking
{
    using System;
    using System.Threading;

    internal class MemoryBarrierEx
    {
        internal static void Run()
        {
            var foo = new Foo();
            var t1 = new Thread(foo.A);

            var t2 = new Thread(foo.B);

            t1.Start();
            t2.Start();

            Console.WriteLine("Press enter to terminate!");
            Console.ReadLine();
        }
    }
}