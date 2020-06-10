namespace _02TasksDemos
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class Demo08
    {
        public static void Run()
        {
            var t1 = Task.Run(() =>
            {
                using (var wc = new System.Net.WebClient())
                {
                    return wc.DownloadString("https://jsonplaceholder.typicode.com/comments?postId=1");
                }
            });

            var t2 = Task.Run(() =>
            {
                using (var wc = new System.Net.WebClient())
                {
                    return wc.DownloadString("https://jsonplaceholder.typicode.com/comments?postId=2");
                }
            });

            var t3 = Task.Run(() =>
            {
                using (var wc = new System.Net.WebClient())
                {
                    return wc.DownloadString("https://jsonplaceholder.typicode.com/comments?postId=3");
                }
            });

            var t4 = Task.Run(() =>
            {
                using (var wc = new System.Net.WebClient())
                {
                    return wc.DownloadString("https://jsonplaceholder.typicode.com/comments?postId4");
                }
            });

            Task.WaitAll(t1, t2, t3, t4);
        }
    }
}