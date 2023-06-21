using Xunit;

using System;
using System.Threading.Tasks;

namespace S05_ParallelLoops;

public partial class S05_01_InvokeForForeach
{
    [Fact]
    public void For()
    {
        Parallel.For(
            1,
            11,
            x =>
            {
                Console.Write($"{x * x}\t");
            }
        );
        Console.WriteLine();
    }
}
