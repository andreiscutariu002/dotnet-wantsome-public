namespace _12Tasks
{
    using System;
    using System.Threading.Tasks;

    internal class ParallelForeachEx
    {
        public static void Run()
        {
            Parallel.ForEach("Hello, world", Foo);
        }

        private static void Foo(char current, ParallelLoopState state, long index)
        {
            Console.WriteLine($"s[{index}] = {current}");
        }
    }
}