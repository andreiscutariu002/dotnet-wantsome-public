namespace _09DemoSumSolved
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Numerics;
    using System.Threading;

    internal class Program
    {
        private static void Main(string[] args)
        {
            var coresCount = Environment.ProcessorCount;
            var threads = new List<Thread>(coresCount);
            var arrayProcessors = new List<ArrayProcessor>(coresCount);

            var arraySize = 50000000;
            var array = BuildAnArray(arraySize);

            var stopwatch = Stopwatch.StartNew();

            var elementsPerCore = arraySize / coresCount;
            var elementsLeftOver = arraySize % coresCount;

            for (int i = 0; i < coresCount; i++)
            {
                var startIndex = i * elementsPerCore;
                var elementsToProcessCount = elementsPerCore;

                if (i == coresCount - 1)
                {
                    elementsToProcessCount += elementsLeftOver; // last thread to calculate the left over
                }

                var arrayProcessor = new ArrayProcessor(array, startIndex, elementsToProcessCount);
                arrayProcessors.Add(arrayProcessor);

                var thread = new Thread(arrayProcessor.CalculateSum);
                threads.Add(thread);
                thread.Start();
            }

            BigInteger totalSum = 0;
            for (int i = 0; i < threads.Count; i++)
            {
                threads[i].Join();

                totalSum += arrayProcessors[i].Sum;
            }

            stopwatch.Stop();

            Console.WriteLine($"Elapsed time: {stopwatch.Elapsed.TotalMilliseconds} ms");
            Console.WriteLine($"Sum: {totalSum}");
        }

        public static int[] BuildAnArray(int size)
        {
            var array = new int[size];

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = i;
            }

            return array;
        }
    }
}
