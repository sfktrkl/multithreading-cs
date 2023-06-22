using Xunit;

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace S06_ParallelLINQ;

public partial class S06_01_AsParallelQuery
{
    const int count = 50;
    IEnumerable<int> items = Enumerable.Range(1, count).ToArray();

    [Fact]
    public void AsParallel()
    {
        // now we can get the cubed value of each element in the array using
        var results = new int[count];
        items
            .AsParallel()
            .ForAll(x =>
            {
                int newValue = x * x * x;
                Console.Write($"{newValue} ({Task.CurrentId})\t");
                results[x - 1] = newValue;
            });
        Console.WriteLine();

        foreach (var i in results)
            Console.Write($"{i}\t");
        Console.WriteLine();
    }
}
