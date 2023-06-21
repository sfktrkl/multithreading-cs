using Xunit;

using System;
using System.Threading.Tasks;

namespace S05_ParallelLoops;

public partial class S05_02_BreakingStopping
{
    private static void Stop()
    {
        ParallelLoopResult result = Parallel.For(
            0,
            20,
            (int x, ParallelLoopState state) =>
            {
                Console.Write($"{x}[{Task.CurrentId}]\t");
                if (x == 10)
                    state.Stop(); // stop execution as soon as possible
            }
        );

        Console.WriteLine();
        Console.WriteLine($"Was loop completed? {result.IsCompleted}");
    }

    [Fact]
    public void Stopping()
    {
        Stop();
    }
}
