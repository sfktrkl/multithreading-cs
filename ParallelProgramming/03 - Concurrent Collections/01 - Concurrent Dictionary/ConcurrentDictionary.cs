using Xunit;

using System;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace S03_ConcurrentCollections;

public partial class S03_01_ConcurrentDictionary
{
    private static ConcurrentDictionary<string, string> capitals =
        new ConcurrentDictionary<string, string>();

    public static void AddParis()
    {
        // there is no add, since you don't know if it will succeed
        bool success = capitals.TryAdd("France", "Paris");

        string who = Task.CurrentId.HasValue ? ("Task " + Task.CurrentId) : "Main thread";
        Console.WriteLine($"{who} {(success ? "added" : "did not add")} the element.");
    }

    [Fact]
    public void ConcurrentDictionary()
    {
        Task.Factory.StartNew(AddParis).Wait();
        Task.Factory.StartNew(AddParis).Wait();

        Task.Factory.StartNew(() =>
        {
            var s = capitals.AddOrUpdate("Russia", "Leningrad", (k, old) => old + " --> Leningrad");
            Console.WriteLine("The capital of Russia is " + capitals["Russia"]);
        });
        Task.Factory.StartNew(() =>
        {
            var s = capitals.AddOrUpdate("Russia", "Moscow", (k, old) => old + " --> Moscow");
            Console.WriteLine("The capital of Russia is " + capitals["Russia"]);
        });

        Task.Factory.StartNew(() =>
        {
            var capital = capitals.GetOrAdd("Sweden", "Uppsala");
            Console.WriteLine($"The capital of Sweden is {capital}.");
        });
        Task.Factory.StartNew(() =>
        {
            var capital = capitals.GetOrAdd("Sweden", "Stockholm");
            Console.WriteLine($"The capital of Sweden is {capital}.");
        });

        Task.Factory.StartNew(() =>
        {
            var remove = "Russia";
            string? removed;
            if (capitals.TryRemove(remove, out removed))
                Console.WriteLine($"We just removed {removed}");
            else
                Console.WriteLine($"Failed to remove capital of {remove}");
        });
    }
}
