using Xunit;

using System;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace S05_ParallelLoops;

public partial class S05_04_Partitioning
{
    const int count = 100000;
    int[] results = new int[count];
    IEnumerable<int> values = Enumerable.Range(0, count);

    [Fact]
    public void SquareEachValueChunked()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        // rangeSize = size of each subrange
        var part = Partitioner.Create(0, count, 10000);
        Parallel.ForEach(
            part,
            range =>
            {
                for (int i = range.Item1; i < range.Item2; i++)
                    results[i] = (int)Math.Pow(i, 2);
            }
        );

        stopwatch.Stop();
        Console.WriteLine(
            $"SquareEachValueChunked: {stopwatch.Elapsed.TotalMilliseconds} milliseconds"
        );
    }
}
