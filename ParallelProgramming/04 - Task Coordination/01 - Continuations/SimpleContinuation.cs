using Xunit;

using System;
using System.Threading.Tasks;

namespace S04_TaskCoordination;

public partial class S04_01_Continuations
{
    [Fact]
    public void SimpleContinuation()
    {
        var task = Task.Factory.StartNew(() =>
        {
            Console.WriteLine($"{Task.CurrentId} - Boil water");
        });

        var task2 = task.ContinueWith(t =>
        {
            Console.WriteLine($"{Task.CurrentId} - Pour into cup");
        });

        task2.Wait();
    }
}
