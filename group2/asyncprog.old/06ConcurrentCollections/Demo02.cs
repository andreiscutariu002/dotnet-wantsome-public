namespace _06ConcurrentCollections
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading.Tasks;

    internal class Demo02
    {
        public static async Task Run()
        {
            var bCollection = new BlockingCollection<int>(2);

            bCollection.Add(1);
            bCollection.Add(2);

            var item = bCollection.Take();
            item = bCollection.Take();

            if (bCollection.TryTake(out item, TimeSpan.FromSeconds(5)))
                Console.WriteLine(item);
            else
                Console.WriteLine("No item removed");
        }
    }
}