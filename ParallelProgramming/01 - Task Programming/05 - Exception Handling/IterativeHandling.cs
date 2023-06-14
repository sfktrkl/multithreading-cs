using Xunit;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace S01_TaskProgramming;

public partial class S01_05_ExceptionHandling
{
    private void CancelTasks()
    {
        var cts = new CancellationTokenSource();
        var token = cts.Token;
        var t = Task.Factory.StartNew(
            () =>
            {
                while (true)
                {
                    token.ThrowIfCancellationRequested();
                    Thread.Sleep(100);
                }
            },
            token
        );

        var t2 = Task.Factory.StartNew(() =>
        {
            throw new NotImplementedException();
        });

        cts.Cancel();

        try
        {
            Task.WaitAll(t, t2);
        }
        catch (AggregateException ae)
        {
            // handle exceptions depending on whether they were expected or
            // handles all expected exceptions ('return true'), throws the
            // unhandled ones back as an AggregateException
            ae.Handle(e =>
            {
                if (e is OperationCanceledException)
                {
                    Console.WriteLine("Whoops, tasks were canceled.");
                    return true; // exception was handled
                }
                else
                {
                    Console.WriteLine($"Something went wrong: {e.Message}");
                    return false; // exception was NOT handled
                }
            });
        }
        finally
        {
            // what happened to the tasks?
            Console.WriteLine("\tfaulted\tcompleted\tcancelled");
            Console.WriteLine($"t\t{t.IsFaulted}\t\t{t.IsCompleted}\t\t\t{t.IsCanceled}");
            Console.WriteLine($"t2\t{t2.IsFaulted}\t\t{t2.IsCompleted}\t\t\t{t2.IsCanceled}");
        }
    }

    [Fact]
    public void IterativeHandling()
    {
        try
        {
            CancelTasks();
        }
        catch (AggregateException ae)
        {
            Console.WriteLine("Some exceptions we didn't expect:");
            foreach (var e in ae.InnerExceptions)
                Console.WriteLine($" - {e.GetType()}");
        }
    }
}
