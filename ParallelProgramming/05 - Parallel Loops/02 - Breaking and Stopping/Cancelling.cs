using Xunit;

using System;
using System.Threading.Tasks;
using System.Threading;

namespace S05_ParallelLoops;

public partial class S05_02_BreakingStopping
{
    private static void Cancel()
    {
        var cts = new CancellationTokenSource();

        var po = new ParallelOptions { CancellationToken = cts.Token };
        ParallelLoopResult result = Parallel.For(
            0,
            20,
            po,
            (int x, ParallelLoopState state) =>
            {
                Console.Write($"{x}[{Task.CurrentId}]\t");
                if (x == 10)
                    cts.Cancel();
            }
        );
    }

    [Fact]
    public void Cancelling()
    {
        try
        {
            Cancel();
        }
        catch (OperationCanceledException e)
        {
            Console.WriteLine();
            Console.WriteLine(e.Message);
        }
    }
}
