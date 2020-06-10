namespace _12TasksCancellation
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;

            try
            {
                CreateCancelledTask3();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                Console.ReadKey();
            }
        }

        public static void CreateCancelledTask()
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;
            tokenSource.Cancel();
            Task.Run(() => Console.WriteLine("Hello from Task"), token);
        }

        public static void CreateCancelledTask2()
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;

            var t = Task.Run(() =>
            {
                var capturedToken = token;

                for (int i = 0; i < 100; i++)
                {
                    Console.WriteLine($"Step {i}.");

                    Task.Delay(TimeSpan.FromSeconds(1)).Wait();

                    if (capturedToken.IsCancellationRequested)
                    {
                        Console.WriteLine($"Cancelled at step {i}.");
                        
                        //break;

                        capturedToken.ThrowIfCancellationRequested();
                    }
                }
            }, token);

            Task.Delay(TimeSpan.FromSeconds(2)).Wait();

            tokenSource.Cancel();

            //t.Wait();

            Console.ReadKey();
        }

        public static void CreateCancelledTask3()
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;

            var t = Task.Factory.StartNew(stateObject =>
            {
                var castedToken = (CancellationToken)stateObject;

                castedToken.Register(() =>
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"You can cancel other things in progress in your task (http call, db operation, etc..). " +
                                      $"This callback is called when token was cancelled!");
                    Console.ForegroundColor = ConsoleColor.White;
                });

                for (int i = 0; i < 100; i++)
                {
                    Console.WriteLine($"Step {i}.");

                    Task.Delay(TimeSpan.FromSeconds(1)).Wait();

                    token.ThrowIfCancellationRequested();
                }
            }, token);

            Task.Delay(TimeSpan.FromSeconds(5)).Wait();

            tokenSource.Cancel();

            Console.ReadKey();
        }
    }
}
