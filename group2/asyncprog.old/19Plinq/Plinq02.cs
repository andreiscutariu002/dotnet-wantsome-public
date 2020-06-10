namespace _19Plinq
{
    using System;
    using System.Linq;

    public class Plinq02
    {
        public static void Run()
        {
            var nums = Enumerable.Range(10, 10000);

            var query = from num in nums.AsParallel()
                where num % 10 == 0
                select num;

            // Process the results as each thread completes
            query.ForAll((e) =>
            {
                Console.WriteLine(e);
            });

            foreach (var i in query)
            {
                Console.WriteLine(i);
            }
        }
    }
}