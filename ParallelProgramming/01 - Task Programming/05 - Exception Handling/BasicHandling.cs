using Xunit;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace S01_TaskProgramming;

public partial class S01_05_ExceptionHandling
{
    [Fact]
    public static void BasicHandling()
    {
        var t = Task.Factory.StartNew(() =>
        {
            throw new InvalidOperationException("Can't do this!") { Source = "t" };
        });

        var t2 = Task.Factory.StartNew(() =>
        {
            var e = new AccessViolationException("Can't access this!");
            e.Source = "t2";
            throw e;
        });

        try
        {
            Task.WaitAll(t, t2);
        }
        catch (AggregateException ae)
        {
            foreach (Exception e in ae.InnerExceptions)
            {
                Console.WriteLine($"Exception {e.GetType()} from {e.Source}.");
            }
        }
    }
}
