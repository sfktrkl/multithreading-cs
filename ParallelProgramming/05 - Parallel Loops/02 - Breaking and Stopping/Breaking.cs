using Xunit;

using System;
using System.Threading.Tasks;

namespace S05_ParallelLoops;

public partial class S05_02_BreakingStopping
{
    private static void Break()
    {
        ParallelLoopResult result = Parallel.For(
            0,
            20,
            (int x, ParallelLoopState state) =>
            {
                Console.Write($"{x}[{Task.CurrentId}]\t");
                if (x == 10)
                    state.Break(); // request that loop stop execution of iterations beyond current iteration asap
            }
        );

        Console.WriteLine();
        Console.WriteLine($"Was loop completed? {result.IsCompleted}");
        if (result.LowestBreakIteration.HasValue)
            Console.WriteLine($"Lowest break iteration: {result.LowestBreakIteration}");
    }

    [Fact]
    public void Breaking()
    {
        Break();
    }
}
