namespace _12Tasks
{
    using System;
    using System.Threading.Tasks;

    internal class ParallelForeachBreakEx
    {
        public static void Run()
        {
            var result = Parallel.ForEach("Hello, world world world world world world", Foo);

            Console.WriteLine(result.IsCompleted);
        }

        private static void Foo(char current, ParallelLoopState loopState, long index)
        {
            if (current == ',')
            {
                loopState.Break();;
            }

            Console.WriteLine($"s[{index}] = {current}");
        }
    }
}