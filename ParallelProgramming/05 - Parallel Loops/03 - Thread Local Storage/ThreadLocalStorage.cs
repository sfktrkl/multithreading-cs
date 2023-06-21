using Xunit;

using System;
using System.Threading.Tasks;
using System.Threading;

namespace S05_ParallelLoops;

public partial class S05_03_ThreadLocalStorage
{
    [Fact]
    public void ThreadLocalStorage()
    {
        int sum = 0;
        Parallel.For(
            1,
            1001,
            () => 0, // initialize local state
            (x, state, tls) =>
            {
                tls += x;
                Console.WriteLine($"Task {Task.CurrentId} has sum {tls}");
                return tls;
            },
            partialSum =>
            {
                Console.WriteLine($"Partial value of task {Task.CurrentId} is {partialSum}");
                Interlocked.Add(ref sum, partialSum);
            }
        );
        Console.WriteLine($"Sum of 1..100 = {sum}");
    }
}
