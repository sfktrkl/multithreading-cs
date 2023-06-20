using Xunit;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace S04_TaskCoordination;

public partial class S04_03_Barrier
{
    static Barrier barrier = new Barrier(
        2,
        b =>
        {
            Console.WriteLine($"Phase {b.CurrentPhaseNumber} is finished.");
        }
    );

    public static void Water()
    {
        Thread.Sleep(1000);
        Console.WriteLine("Putting the kettle on (takes a bit longer).");
        barrier.SignalAndWait(); // signaling and waiting fused
        Console.WriteLine("Putting water into cup.");
        barrier.SignalAndWait();
        Console.WriteLine("Putting the kettle away.");
    }

    public static void Cup()
    {
        Console.WriteLine("Finding the nicest tea cup (only takes a second).");
        barrier.SignalAndWait();
        Console.WriteLine("Adding tea.");
        barrier.SignalAndWait();
        Console.WriteLine("Adding sugar");
    }

    [Fact]
    public void Barrier()
    {
        var water = Task.Factory.StartNew(Water);
        var cup = Task.Factory.StartNew(Cup);

        var tea = Task.Factory.ContinueWhenAll(
            new[] { water, cup },
            tasks =>
            {
                Console.WriteLine("Enjoy your cup of tea.");
            }
        );

        tea.Wait();
    }
}
