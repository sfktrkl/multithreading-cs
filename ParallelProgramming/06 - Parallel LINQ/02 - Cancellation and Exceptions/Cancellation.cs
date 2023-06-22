using Xunit;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace S06_ParallelLINQ;

public partial class S06_02_CancellationExceptions
{
    [Fact]
    public void Cancellation()
    {
        var cts = new CancellationTokenSource();

        var items = Enumerable.Range(1, 20);
        var results = items
            .AsParallel()
            .WithCancellation(cts.Token)
            .Select(i =>
            {
                double result = Math.Log10(i);

                Thread.Sleep((int)(result * 1000));
                Console.WriteLine($"i = {i}, tid = {Task.CurrentId}");
                return result;
            });

        try
        {
            foreach (var c in results)
            {
                if (c > 1)
                    cts.Cancel();
                Console.WriteLine($"result = {c}");
            }
        }
        catch (OperationCanceledException e)
        {
            Console.WriteLine($"Cancelled {e.Message}");
        }
    }
}
