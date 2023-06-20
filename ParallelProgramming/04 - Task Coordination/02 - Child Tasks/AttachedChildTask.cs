using Xunit;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace S04_TaskCoordination;

public partial class S04_02_ChildTasks
{
    [Fact]
    public void AttachedChildTask()
    {
        var parent = new Task(() =>
        {
            Console.WriteLine("Parent task starting...");

            var child = new Task(
                () =>
                {
                    Console.WriteLine("Child task starting...");
                    Console.WriteLine("Child task finished.");

                    throw new Exception();
                },
                TaskCreationOptions.AttachedToParent
            );
            var failHandler = child.ContinueWith(
                t =>
                {
                    Console.WriteLine($"Unfortunately, Task {t.Id}'s state is {t.Status}");
                },
                TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnFaulted
            );
            var completionHandler = child.ContinueWith(
                t =>
                {
                    Console.WriteLine($"Hooray, Task {t.Id}'s state is {t.Status}");
                },
                TaskContinuationOptions.AttachedToParent
                    | TaskContinuationOptions.OnlyOnRanToCompletion
            );
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
