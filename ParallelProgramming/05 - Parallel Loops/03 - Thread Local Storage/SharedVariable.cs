using Xunit;

using System;
using System.Threading.Tasks;
using System.Threading;

namespace S05_ParallelLoops;

public partial class S05_03_ThreadLocalStorage
{
    [Fact]
    public void SharedVariable()
    {
        int sum = 0;
        Parallel.For(
            1,
            1001,
            x =>
            {
                Console.Write($"[{x}] t={Task.CurrentId}\t");
                Interlocked.Add(ref sum, x);
                // concurrent access to sum from all these threads is inefficient
                // all tasks can add up their respective values, then add sum to total sum
            }
        );
        Console.WriteLine($"\nSum of 1..100 = {sum}");
    }
}
