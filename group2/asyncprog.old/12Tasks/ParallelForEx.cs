namespace _12Tasks
{
    using System;
    using System.Threading.Tasks;

    internal class ParallelForEx
    {
        public static void Run()
        {
            Parallel.For(0, 100, Foo);
        }

        private static void Foo(int i)
        {
            Console.WriteLine(i);
        }
    }
}