using Xunit;

using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace S05_ParallelLoops;

public partial class S05_01_InvokeForForeach
{
    public static IEnumerable<int> Range(int start, int end, int step)
    {
        for (int i = start; i < end; i += step)
        {
            yield return i;
        }
    }

    [Fact]
    public void ForEach()
    {
        Parallel.ForEach(Range(1, 20, 3), Console.WriteLine);

        string[] letters = { "oh", "what", "a", "night" };
        var po = new ParallelOptions();
        po.MaxDegreeOfParallelism = 2;
        Parallel.ForEach(
            letters,
            po,
            letter =>
            {
                Console.WriteLine($"{letter} has length {letter.Length} (task {Task.CurrentId})");
            }
        );
    }
}
