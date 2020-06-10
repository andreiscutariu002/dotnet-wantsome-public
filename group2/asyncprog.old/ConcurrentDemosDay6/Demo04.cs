namespace ConcurrentDemosDay6
{
    using System;
    using System.Collections.Concurrent;

    internal class Demo04
    {
        public static void Run()
        {
            BlockingCollection<int> bCollection = new BlockingCollection<int>(boundedCapacity: 2);

            bCollection.Add(1);
            bCollection.Add(2);

            if (bCollection.TryAdd(3, TimeSpan.FromSeconds(1)))
            {
                Console.WriteLine("Item added");
            }
            else
            {
                Console.WriteLine("Item does not added");
            }
        }
    }
}