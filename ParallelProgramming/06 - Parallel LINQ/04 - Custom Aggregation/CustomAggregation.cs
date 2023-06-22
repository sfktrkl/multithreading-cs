using Xunit;

using System;
using System.Linq;

namespace S06_ParallelLINQ;

public partial class S06_04_CustomAggregation
{
    [Fact]
    public void CustomAggregation()
    {
        int sum;
        int count = 10000;

        // some operations in LINQ perform an aggregation
        sum = Enumerable.Range(1, count).Sum();
        Console.WriteLine($"Sum is {sum}");
        sum = ParallelEnumerable.Range(1, count).Sum();
        Console.WriteLine($"Sum is {sum}");

        // Sum is just a specialized case of Aggregate
        sum = Enumerable.Range(1, count).Aggregate(0, (i, acc) => i + acc);
        Console.WriteLine($"Sum is {sum}");

        sum = ParallelEnumerable
            .Range(1, count)
            .Aggregate(
                0, // initial seed for calculations
                (partialSum, i) => partialSum += i, // per task
                (total, subtotal) => total += subtotal, // combine all tasks
                i => i
            ); // final result processing
        Console.WriteLine($"Sum is {sum}");
    }
}
