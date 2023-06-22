using Xunit;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace S06_ParallelLINQ;

public partial class S06_02_CancellationExceptions
{
    [Fact]
    public void Exceptions()
    {
        var items = Enumerable.Range(1, 20);
        var results = items
            .AsParallel()
            .Select(i =>
            {
                double result = Math.Log10(i);
                if (result > 1)
                    throw new InvalidOperationException();

                Thread.Sleep((int)(result * 1000));
                Console.WriteLine($"i = {i}, tid = {Task.CurrentId}");
                return result;
            });

        try
        {
            foreach (var c in results)
                Console.WriteLine($"result = {c}");
        }
        catch (AggregateException ae)
        {
            ae.Handle(e =>
            {
                Console.WriteLine($"{e.GetType().Name}: {e.Message}");
                return true;
            });
        }
    }
}
