namespace FileProcessor
{
    using System;
    using System.Collections.Concurrent;
    using System.IO;
    using System.Threading;

    public class Publisher
    {
        private readonly BlockingCollection<string> blockingCollection;
        private readonly CancellationToken token;

        public Publisher(BlockingCollection<string> blockingCollection, CancellationToken token)
        {
            this.blockingCollection = blockingCollection;
            this.token = token;
        }

        public void Start(string path)
        {
            FileSystemWatcher watcher = new FileSystemWatcher();

            watcher.Path = path;
            watcher.NotifyFilter = NotifyFilters.LastAccess
                                   | NotifyFilters.LastWrite
                                   | NotifyFilters.FileName
                                   | NotifyFilters.DirectoryName;

            watcher.Created += (sender, args) =>
            {
                Console.WriteLine($"File read on TID {Thread.CurrentThread.ManagedThreadId}");
                this.blockingCollection.Add(args.FullPath, this.token);
            };

            watcher.EnableRaisingEvents = true;
        }
    }
}