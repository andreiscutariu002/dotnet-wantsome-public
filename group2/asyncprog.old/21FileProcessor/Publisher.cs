    namespace _21FileProcessor
{
    using System;
    using System.Collections.Concurrent;
    using System.IO;
    using System.Threading;

    public class Publisher
    {
        private readonly BlockingCollection<string> queue;

        public Publisher(BlockingCollection<string> queue, CancellationToken ctsToken)
        {
            this.queue = queue;
        }

        public void Start(string directoryPath)
        {
            var watcher = new FileSystemWatcher
            {
                Path = directoryPath,
                NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.LastWrite | NotifyFilters.FileName
            };

            watcher.Created += (sender, e) => this.SendFileToConsumer(e.FullPath);
            watcher.EnableRaisingEvents = true;
        }

        private void SendFileToConsumer(string fullPath)
        {
            Thread.Sleep(TimeSpan.FromMilliseconds(2)); // wait copy process to finish
            this.queue.Add(fullPath);
        }
    }
}