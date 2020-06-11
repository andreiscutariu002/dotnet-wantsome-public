namespace Threads
{
    using System;

    class Ex01
    {
        private static void Go()
        {
            int cores = Environment.ProcessorCount;

            var arraySize = 50000000; // 50 000 000
            var array = BuildAnArray(arraySize);

            var sum = 0;

            for (int i = 0; i < array.Length; i++)
            {
                sum += array[i];
            }

            Console.WriteLine(sum);
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
