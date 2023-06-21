using Xunit;

using System;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;

namespace S05_ParallelLoops;

public partial class S05_04_Partitioning
{
    [Fact]
    public void SquareEachValue()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        Parallel.ForEach(
            values,
            x =>
            {
                results[x] = (int)Math.Pow(x, 2);
            }
        );

        stopwatch.Stop();
        Console.WriteLine($"SquareEachValue: {stopwatch.Elapsed.TotalMilliseconds} milliseconds");
    }
}
