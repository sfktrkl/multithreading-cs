using Xunit;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace S04_TaskCoordination;

public partial class S04_04_ManualAndAutoResetEvent
{
    [Fact]
    public void AutoResetEvent()
    {
        var evt = new AutoResetEvent(false);

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
            evt.WaitOne();
            Console.WriteLine("Here is your tea!");

            var ok = evt.WaitOne(1000);
            if (ok)
                Console.WriteLine("That was a nice cup of tea!");
            else
                Console.WriteLine("No tea for you.");
        });

        makeTea.Wait();
    }
}
