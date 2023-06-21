using Xunit;

using System;
using System.Threading.Tasks;

namespace S05_ParallelLoops;

public partial class S05_01_InvokeForForeach
{
    [Fact]
    public void Invoke()
    {
        var a = new Action(() => Console.WriteLine($"First {Task.CurrentId}"));
        var b = new Action(() => Console.WriteLine($"Second {Task.CurrentId}"));
        var c = new Action(() => Console.WriteLine($"Third {Task.CurrentId}"));
        Parallel.Invoke(a, b, c);
    }
}
