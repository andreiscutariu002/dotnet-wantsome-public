namespace _05TplDemos
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Threading.Tasks;

    internal class Demo07
    {
        internal static async Task Run()
        {
            var source = Enumerable.Range(0, 1000000).ToArray();

            // Partition the entire source array.
            var rangePartitioner = Partitioner.Create(0, source.Length);
            var nr = 0;
            foreach (var partition in rangePartitioner.GetDynamicPartitions())
            {
                Console.WriteLine($"{nr++} Partition: {partition.Item1} - {partition.Item2}");
            }

            //double[] results = new double[source.Length];

            //// Loop over the partitions in parallel.
            //Parallel.ForEach(rangePartitioner, (range, loopState) =>
            //{
            //    Console.WriteLine($"Partition: {range.Item1} - {range.Item2}");

            //    // Loop over each range element without a delegate invocation.
            //    for (int i = range.Item1; i < range.Item2; i++)
            //    {
            //        results[i] = source[i] * Math.PI;
            //    }
            //});
        }
    }
}