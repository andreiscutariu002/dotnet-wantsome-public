using System;
using System.Threading;
using System.Threading.Tasks;

internal class Program
{
    private static string result;

    private static void Main()
    {
        var t = SaySomething();
        Console.WriteLine(result);
    }

    private static async Task<string> SaySomething()
    {
        Thread.Sleep(TimeSpan.FromSeconds(5));
        result = "Hello world!";
        return "Something";
    }
}
