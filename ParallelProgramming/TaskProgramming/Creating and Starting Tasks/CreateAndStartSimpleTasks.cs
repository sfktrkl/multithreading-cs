using Xunit;

using System;
using System.Threading.Tasks;

namespace TaskProgramming;

public partial class CreatingStartingTasks
{
    private static void Write(char c)
    {
        int i = 1000;
        while (i-- > 0)
        {
            Console.Write(c);
        }
    }

    [Fact]
    public void CreateAndStartSimpleTasks()
    {
        // Task is a unit of work in .NET
        Task.Factory.StartNew(() =>
        {
            Write('-');
        });

        // The argument is an action.
        // It can be a delegate, a lambda or an anonymous method.
        Task t = new Task(() => Write('?'));
        t.Start(); // Task doesn't start automatically!

        Write('.');
    }
}
