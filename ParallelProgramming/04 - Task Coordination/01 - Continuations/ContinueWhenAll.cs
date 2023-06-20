using Xunit;

using System;
using System.Threading.Tasks;

namespace S04_TaskCoordination;

public partial class S04_01_Continuations
{
    [Fact]
    public void ContinueWhenAll()
    {
        var task = Task.Factory.StartNew(() => "Task 1");
        var task2 = Task.Factory.StartNew(() => "Task 2");

        var task3 = Task.Factory.ContinueWhenAll(
            new[] { task, task2 },
            tasks =>
            {
                Console.WriteLine("Tasks completed:");
                foreach (var t in tasks)
                    Console.WriteLine(" - " + t.Result);
                Console.WriteLine("All tasks done");
            }
        );

        task3.Wait();
    }
}
