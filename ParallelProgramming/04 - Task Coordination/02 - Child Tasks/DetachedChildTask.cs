using Xunit;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace S04_TaskCoordination;

public partial class S04_02_ChildTasks
{
    [Fact]
    public void DetachedChildTask()
    {
        var parent = new Task(() =>
        {
            Console.WriteLine("Parent task starting...");

            var child = new Task(() =>
            {
                Console.WriteLine("Child task starting...");
                Thread.Sleep(3000);
                Console.WriteLine("Child task finished.");
            });
            child.Start();

            Console.WriteLine("Parent task finished.");
        });

        parent.Start();
        try
        {
            parent.Wait();
        }
        catch (AggregateException ae)
        {
            ae.Handle(e => true);
        }
    }
}
