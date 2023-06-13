using Xunit;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskProgramming;

public partial class CancellingTasks
{
    [Fact]
    public void MonitoringCancellation()
    {
        var cts = new CancellationTokenSource();
        var token = cts.Token;

        // register a delegate to fire
        token.Register(() =>
        {
            Console.WriteLine("Cancelation has been requested.");
        });

        Task t = new Task(() =>
        {
            int i = 0;
            while (true)
            {
                if (token.IsCancellationRequested) // 1. Soft exit
                // RanToCompletion
                {
                    break;
                }
                else
                {
                    Console.Write($"{i++}\t");
                    Thread.Sleep(100);
                }
            }
        });
        t.Start();

        // canceling multiple tasks
        Task t2 = Task.Factory.StartNew(
            () =>
            {
                char c = 'a';
                while (true)
                {
                    // alternative to what's below
                    token.ThrowIfCancellationRequested(); // 2. Hard exit, Canceled

                    if (token.IsCancellationRequested) // same as above, start HERE
                    {
                        // release resources, if any
                        throw new OperationCanceledException(
                            "No longer interested in printing letters."
                        );
                    }
                    else
                    {
                        Console.Write($"{c++}\t");
                        Thread.Sleep(200);
                    }
                }
            },
            token
        ); // don't do token, show R# magic

        // cancellation on a wait handle
        Task.Factory.StartNew(() =>
        {
            token.WaitHandle.WaitOne();
            Console.WriteLine("Wait handle released, thus cancelation was requested");
        });

        Thread.Sleep(1000);
        cts.Cancel();

        Thread.Sleep(1000); // cancelation is non-instant

        Console.WriteLine(
            $"Task has been canceled. The status of the canceled task 't' is {t.Status}."
        );
        Console.WriteLine(
            $"Task has been canceled. The status of the canceled task 't2' is {t2.Status}."
        );
        Console.WriteLine($"t.IsCanceled = {t.IsCanceled}, t2.IsCanceled = {t2.IsCanceled}");
    }
}
