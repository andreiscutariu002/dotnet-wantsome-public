namespace _01ThreadingDemos
{
    using System;
    using System.IO;
    using System.Threading;

    public class FileHelper
    {
        public static void ReadFile()
        {
            ReadFile("..\\..\\..\\..\\..\\TextFile.txt");
        }

        public static void ReadFile(string path)
        {
            var counter = 0;
            string line;

            var file = new StreamReader(path);
            while ((line = file.ReadLine()) != null)
            {
                Console.WriteLine($"[T_ID:{Thread.CurrentThread.ManagedThreadId}] {line}");
                Thread.Sleep(TimeSpan.FromMilliseconds(20)); // add some load
                counter++;
            }

            file.Close();

            Console.WriteLine($"[T_ID:{Thread.CurrentThread.ManagedThreadId}] There were {0} lines.", counter);
        }
    }
}