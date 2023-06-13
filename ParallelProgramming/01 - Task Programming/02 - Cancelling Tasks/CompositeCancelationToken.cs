using Xunit;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace S01_TaskProgramming;

public partial class S01_02_CancellingTasks
{
    [Fact]
    public void CompositeCancelationToken()
    {
        // it's possible to create a 'composite' cancelation source that involves several tokens
        var planned = new CancellationTokenSource();
        var preventative = new CancellationTokenSource();
        var emergency = new CancellationTokenSource();

        // make a token source that is linked on their tokens
        var paranoid = CancellationTokenSource.CreateLinkedTokenSource(
            planned.Token,
            preventative.Token,
            emergency.Token
        );

        Task.Factory.StartNew(
            () =>
            {
                int i = 0;
                while (true)
                {
                    paranoid.Token.ThrowIfCancellationRequested();
                    Console.Write($"{i++}\t");
                    Thread.Sleep(100);
                }
            },
            paranoid.Token
        );

        paranoid.Token.Register(() => Console.WriteLine("Cancelation requested"));

        // use any of the aforementioned token soures
        Thread.Sleep(1000);
        emergency.Cancel();
    }
}
