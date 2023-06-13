using Xunit;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace S01_TaskProgramming;

public partial class S01_02_CancellingTasks
{
    [Fact]
    public void CancellableTasks()
    {
        var cts = new CancellationTokenSource();
        var token = cts.Token;
        Task t = new Task(() =>
        {
            int i = 0;
            while (true)
            {
                if (token.IsCancellationRequested) // task cancelation is cooperative, no-one kills your thread
                    break;
                else
                    Console.Write($"{i++}\t");
            }
        });
        t.Start();

        // don't forget CancellationToken.None
        Thread.Sleep(100);
        cts.Cancel();

        Console.WriteLine("Task has been canceled.");
    }
}
