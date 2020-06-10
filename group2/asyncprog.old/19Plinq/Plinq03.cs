namespace _19Plinq
{
    using System;
    using System.Linq;

    public class Plinq03
    {
        public static void Run()
        {
            var customers = PlinqDataSample.GetCustomers();

            // Take the first 20, preserving the original order
            var firstTwentyCustomers = customers
                .AsParallel()
                .AsOrdered()
                .Take(20);

            foreach (var c in firstTwentyCustomers)
                Console.WriteLine("{0} ", c.CustomerID);

            Console.WriteLine();

            // All elements in reverse order.
            var reverseOrder = customers
                .AsParallel()
                .AsOrdered()
                .Reverse();

            foreach (var v in reverseOrder)
                Console.WriteLine("{0} ", v.CustomerID);
        }
    }
}