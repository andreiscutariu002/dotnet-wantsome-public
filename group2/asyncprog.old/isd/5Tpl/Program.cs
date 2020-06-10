using System;

namespace _5Tpl
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    class Program
    {
        static async Task Main(string[] args)
        {
            Demo06.Run();
        }
    }

    public class Demo06
    {
        public static void Run()
        {
            var nums = Enumerable.Range(1, 100);

            // Replace NotBuffered with AutoBuffered 
            // or FullyBuffered to compare behavior.
            var scanLines = from n in nums.AsParallel()
                // .WithMergeOptions(ParallelMergeOptions.AutoBuffered)
                //.WithMergeOptions(ParallelMergeOptions.NotBuffered)
                .WithMergeOptions(ParallelMergeOptions.FullyBuffered)
                where n % 2 == 0
                select ExpensiveFunc(n);

            Stopwatch sw = Stopwatch.StartNew();
            foreach (var line in scanLines)
            {
                Console.WriteLine(line);
            }

            Console.WriteLine("Elapsed time: {0} ms. Press any key to exit.",
                sw.ElapsedMilliseconds);
            Console.ReadKey();
        }


        static string ExpensiveFunc(int i)
        {
            Thread.Sleep(TimeSpan.FromMilliseconds(300));
            return $"{i} ****************************";
        }
    }

    public class Demo05
    {
        internal static void Run()
        {
            DateTime startDateTime = DateTime.Now;

            var source = Enumerable.Range(1, 1000).ToList();

            // Opt in to PLINQ with AsParallel.
            var evenNums = source.Where(num =>
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(1));
                return num % 2 == 0;
            }).OrderBy(x => x);

            Console.WriteLine("{0} even numbers out of {1} total",
                evenNums.Count(), source.Count());

            DateTime endDateTime = DateTime.Now;

            TimeSpan span = endDateTime - startDateTime;
            int ms = (int)span.TotalMilliseconds;
            Console.WriteLine(@"Time in ms {0}", ms);
        }
    }

    public class Demo01
    {
        internal static void Run()
        {
            ParallelOptions parallelOptions = new ParallelOptions
            {
                MaxDegreeOfParallelism = 4,
                TaskScheduler = TaskScheduler.FromCurrentSynchronizationContext()
            };

            try
            {
                Parallel.Invoke(parallelOptions,
                    SomeAction, // Invoking Normal Method
                    SomeAction, // Invoking Normal Method
                    delegate // Invoking an inline delegate 
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(2));
                        Console.WriteLine($"Method 2, Thread={Thread.CurrentThread.ManagedThreadId}");
                    },
                    () => // Invoking a lambda expression
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(2));
                        Console.WriteLine($"Method 3, Thread={Thread.CurrentThread.ManagedThreadId}");
                        Task.Delay(TimeSpan.FromSeconds(2)).Wait();
                    }
                );
            }
            catch (AggregateException e)
            {
            }

            Console.WriteLine("Press any key to exist.");
            Console.ReadKey();
        }

        private static void SomeAction()
        {
            throw new InvalidCastException();
            Thread.Sleep(TimeSpan.FromSeconds(2));
            Console.WriteLine($"Method 1, Thread={Thread.CurrentThread.ManagedThreadId}");
        }
    }

    public class Demo02
    {
        internal static void Run()
        {
            DateTime startDateTime = DateTime.Now;

            var number = 100000000;

            long totalSum = 0;

            object o = new object();

            //// 4
            Parallel.For(0, number, (i, breakLoopState) =>
            {
                // locks = number
                lock (o)
                {
                    totalSum += i;
                }
            });

            Func<long> localInit = () => { return 0; };

            Func<int, ParallelLoopState, long, long> body = (idx, parallelLoopState, subtotal) =>
            {
                subtotal += idx;
                return subtotal;
            };

            Action<long> localFinally = localTotal =>
            {
                // locks = nr threads
                lock (o)
                {
                    totalSum += localTotal;
                }

                //Interlocked.Add(ref totalSum, localTotal);
            };

            Parallel.For(0, number, localInit, body, localFinally);

            //4999999950000000 - lock - 4.6 s

            //4999999950000000 - lock - 0.2 s

            DateTime endDateTime = DateTime.Now;
            Console.WriteLine(@"Parallel For Loop Execution end at : {0}", endDateTime);

            TimeSpan span = endDateTime - startDateTime;
            int ms = (int) span.TotalMilliseconds;
            Console.WriteLine(@"Time Taken to Execute the Loop in ms {0}", ms);
            Console.WriteLine(totalSum);
        }
    }

    public class Demo03
    {
        internal static async Task Run()
        {
            var result = await GetData2("www.google.com", new CancellationToken());
            Console.WriteLine(result);
        }

        static Task<string> GetData(string url, CancellationToken token)
        {
            TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();

            WebClient client = new WebClient();

            client.DownloadStringCompleted += (sender, args) =>
            {
                DownloadStringCompletedEventArgs ev = args;

                if (args.Cancelled)
                {
                    tcs.TrySetCanceled();
                    return;
                }

                if (args.Error != null)
                {
                    tcs.TrySetException(args.Error);
                    return;
                }

                var result = args.Result;
                tcs.TrySetResult(result);
            };

            Uri address;
            try
            {
                address = new Uri(url);
            }
            catch (Exception e)
            {
                tcs.TrySetException(e);
                return tcs.Task;
            }

            client.DownloadStringAsync(address);

            return tcs.Task;
        }

        static Task<string> GetData2(string url, CancellationToken token)
        {
            TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();
            WebClient client = new WebClient();

            Uri address;
            try
            {
                address = new Uri(url);
            }
            catch (Exception e)
            {
                tcs.TrySetException(e);
                return tcs.Task;
            }

            ThreadPool.QueueUserWorkItem(state =>
            {
                var result = client.DownloadString(address);
                tcs.SetResult(result);
            });

            return tcs.Task;
        }
    }

    public class Demo04
    {
        internal static void Run()
        {
            var source = Enumerable.Range(0, 100000).ToArray();

            // Partition the entire source array.
            var rangePartitioner = Partitioner.Create(0, source.Length);

            var idx = 0;

            foreach (var partition in rangePartitioner.GetDynamicPartitions())
            {
                Console.WriteLine($"{++idx} - Partition: {partition.Item1} - {partition.Item2}");
            }

            Console.WriteLine();

            Parallel.ForEach(rangePartitioner, (range, loopState) =>
            {
                Console.WriteLine($"Inside ForEach Partition: {range.Item1} - {range.Item2}");
            });

            Task y = source.ParallelForEachAsync(i => { return Task.Run(()=>
            {
                return 0 - i;
            }); }, 8);
        }
    }

    public static class Demo08
    {
        public static Task ParallelForEachAsync<T>(this IEnumerable<T> source, Func<T, Task> funcBody, int maxDoP = 4)
        {
            async Task AwaitPartition(IEnumerator<T> partition)
            {
                using (partition)
                {
                    while (partition.MoveNext())
                    { await funcBody(partition.Current); }
                }
            }

            return Task.WhenAll(
                Partitioner
                    .Create(source)
                    .GetPartitions(maxDoP)
                    .AsParallel()
                    .Select(p => AwaitPartition(p)));
        }
    }
}
