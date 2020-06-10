namespace TasksDay02
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal class DemoEx5
    {
        internal static void Run()
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = tokenSource.Token;

            cancellationToken.Register(() =>
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"You can cancel other things in progress in your task (http call, db operation, etc..). " +
                                  $"This callback is called when token was cancelled!");
                Console.ForegroundColor = ConsoleColor.White;
            });

            var task = Task.Run(() =>
            {
                var capturedToken = cancellationToken;

                for (int i = 0; i < 100; i++)
                {
                    Console.WriteLine($"Step {i}.");

                    Task.Delay(TimeSpan.FromSeconds(1), capturedToken).Wait(capturedToken);

                    if (capturedToken.IsCancellationRequested)
                    {
                        Console.WriteLine($"Cancelled at step {i}.");

                        break;

                        //capturedToken.ThrowIfCancellationRequested(); // throw an OperationCanceledException.
                    }
                }
            });

            Thread.Sleep(TimeSpan.FromSeconds(2));

            tokenSource.Cancel();

            task.Wait();

            Console.ReadKey();
        }
    }
}