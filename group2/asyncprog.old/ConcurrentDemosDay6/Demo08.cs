namespace ConcurrentDemosDay6
{
    using System;
    using System.Collections.Concurrent;

    internal class Demo08
    {
        public static void Run()
        {
            ConcurrentDictionary<string, string> dictionary = new ConcurrentDictionary<string, string>();
            dictionary.TryAdd("1", "First");
            dictionary.TryAdd("2", "Second");
            dictionary.TryAdd("3", "Third");
            dictionary.TryAdd("4", "Fourth");

            string newValue;

            bool returnTrue = dictionary.TryUpdate("1", "New Value", "First"); //Returns true
            dictionary.TryGetValue("1", out newValue);
            Console.WriteLine(newValue); //Print "New Value"

            bool returnsFalse = dictionary.TryUpdate("2", "New Value 2", "No Value"); //Returns false
            dictionary.TryGetValue("2", out newValue);  //Returns "Second" Old value
            Console.WriteLine(newValue);    //Print "Second"
        }
    }
}