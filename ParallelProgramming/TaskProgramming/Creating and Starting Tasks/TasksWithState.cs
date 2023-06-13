using Xunit;

using System;
using System.Threading.Tasks;

namespace TaskProgramming;

public partial class CreatingStartingTasks
{
    private static void Write(object? s)
    {
        int i = 1000;
        while (i-- > 0)
        {
            Console.Write(s?.ToString());
        }
    }

    [Fact]
    public void TasksWithState()
    {
        // clumsy 'object' approach
        Task t = new Task(Write, "foo");
        t.Start();
        Task.Factory.StartNew(Write, "bar");
    }
}
