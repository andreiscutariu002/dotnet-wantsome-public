namespace _05NonBlocking
{
    using System;
    using System.Threading;

    internal class Foo
    {
        private int answer;
        private bool complete;

        internal void A()
        {
            this.answer = 123;
            Thread.MemoryBarrier();         // Barrier 1

            this.complete = true;
            Thread.MemoryBarrier();         // Barrier 2
        }

        internal void B()
        {
            Thread.MemoryBarrier();         // Barrier 3
            if (this.complete)
            {
                Thread.MemoryBarrier();     // Barrier 4
                Console.WriteLine(this.answer);
            }
        }
    }
}