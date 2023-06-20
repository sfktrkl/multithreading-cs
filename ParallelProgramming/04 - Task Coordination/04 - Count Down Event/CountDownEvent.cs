using Xunit;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace S04_TaskCoordination;

public partial class S04_03_CountDownEvent
{
    private static int taskCount = 5;
    static CountdownEvent cte = new CountdownEvent(taskCount);
    static Random random = new Random();

    [Fact]
    public void CountDownEvent()
    {
        var tasks = new Task[taskCount];
        for (int i = 0; i < taskCount; i++)
        {
            tasks[i] = Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"Entering task {Task.CurrentId}.");
                Thread.Sleep(random.Next(1000));
                cte.Signal(); // also takes a signalcount
                Console.WriteLine($"Exiting task {Task.CurrentId}.");
            });
        }

        var finalTask = Task.Factory.StartNew(() =>
        {
            Console.WriteLine($"Waiting for other tasks in task {Task.CurrentId}");
            cte.Wait();
            Console.WriteLine("All tasks completed.");
        });

        finalTask.Wait();
    }
}
