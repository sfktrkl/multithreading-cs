using Xunit;

using System;
using System.Linq;

namespace S06_ParallelLINQ;

public partial class S06_01_AsParallelQuery
{
    [Fact]
    public void AsOrdered()
    {
        // now let's get an enumeration
        // by default, the sequence is quite different to our nicely laid out array
        // but....                    .AsOrdered()
        // also...                    .AsUnordered()
        var cubes = items.AsParallel().AsOrdered().Select(x => x * x * x);

        // this evaluation is delayed: you actually calculate the values only
        // when iterating or doing ToArray()
        foreach (var i in cubes)
            Console.Write($"{i}\t");
        Console.WriteLine();
    }
}
