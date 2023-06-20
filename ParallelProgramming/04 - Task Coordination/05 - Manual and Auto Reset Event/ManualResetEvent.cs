using Xunit;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace S04_TaskCoordination;

public partial class S04_04_ManualAndAutoResetEvent
{
    [Fact]
    public void ManualResetEvent()
    {
        var evt = new ManualResetEventSlim();

        Task.Factory.StartNew(() =>
        {
            Console.WriteLine("Boiling water...");
            Thread.Sleep(100);
            Console.WriteLine("Water is ready.");
            evt.Set();
        });

        var makeTea = Task.Factory.StartNew(() =>
        {
            Console.WriteLine("Waiting for water...");
            evt.Wait();
            Console.WriteLine("Here is your tea!");
            evt.Wait(); // already set!
            Console.WriteLine("That was a nice cup of tea!");
        });

        makeTea.Wait();
    }
}
