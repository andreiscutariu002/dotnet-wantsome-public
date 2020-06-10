namespace ConcurrentDemosDay6
{
    using System;
    using System.Collections.Concurrent;

    internal class Demo02
    {
        public static void Run()
        {
            var bag = new ConcurrentBag<int>();
            bag.Add(1);
            bag.Add(2);

            foreach (var i in bag) Console.WriteLine(i);

            int item;
            var isSuccess = bag.TryPeek(out item); //isSuccess=True, item = 2


            var isSuccess2 = bag.TryTake(out item); //isSuccess=True, item = 2
        }
    }
}