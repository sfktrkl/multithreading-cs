using Xunit;

using System;
using System.Threading.Tasks;

namespace S05_ParallelLoops;

public partial class S05_02_BreakingStopping
{
    private static void Throw()
    {
        ParallelLoopResult result = Parallel.For(
            0,
            20,
            (int x, ParallelLoopState state) =>
            {
                Console.Write($"{x}[{Task.CurrentId}]\t");
                if (x == 10)
                    throw new Exception(); // execution stops on exception
            }
        );
    }

    [Fact]
    public void Exception()
    {
        try
        {
            Throw();
        }
        catch (AggregateException ae)
        {
            Console.WriteLine();
            ae.Handle(e =>
            {
                Console.WriteLine(e.Message);
                return true;
            });
        }
    }
}
