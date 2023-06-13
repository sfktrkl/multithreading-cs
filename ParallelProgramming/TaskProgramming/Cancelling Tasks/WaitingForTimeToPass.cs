using Xunit;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskProgramming;

public partial class CancellingTasks
{
    [Fact]
    public void WaitingForTimeToPass()
    {
        // we've already seen the classic Thread.Sleep
        var cts = new CancellationTokenSource();
        var token = cts.Token;
        var t = new Task(
            () =>
            {
                Console.WriteLine("You have 5 seconds to disarm this bomb by pressing a key");
                bool canceled = token.WaitHandle.WaitOne(5000);
                Console.WriteLine(canceled ? "Bomb disarmed." : "BOOM!!!!");
            },
            token
        );
        t.Start();

        // unlike sleep and waitone
        // thread does not give up its turn
        Thread.SpinWait(1000);
        Console.WriteLine("Are you still here?");

        Thread.Sleep(100);
        cts.Cancel();
    }
}
