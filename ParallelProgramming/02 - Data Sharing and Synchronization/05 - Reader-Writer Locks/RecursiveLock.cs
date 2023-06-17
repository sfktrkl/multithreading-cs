using Xunit;

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace S02_DataSharingSynchronization;

public partial class S02_05_ReaderWriterLock
{
    [Fact]
    public void RecursiveLock()
    {
        var padlock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

        int x = 0;
        Action<int>? recurse = null;
        recurse = (int i) =>
        {
            if (i > 1)
                return;

            padlock.EnterReadLock();
            Console.WriteLine($"Entered recursive read lock, x = {x}, i = {i}");
            if (recurse is not null)
                recurse(++i);
            padlock.ExitReadLock();
        };

        var tasks = new List<Task>();
        for (int i = 0; i < 10; i++)
        {
            tasks.Add(
                Task.Factory.StartNew(() =>
                {
                    recurse(0);
                })
            );
            tasks.Add(
                Task.Factory.StartNew(() =>
                {
                    padlock.EnterWriteLock();
                    x += 1;
                    Thread.Sleep(100);
                    Console.WriteLine($"Entered write lock, x = {x}");
                    padlock.ExitWriteLock();
                })
            );
        }

        try
        {
            Task.WaitAll(tasks.ToArray());
        }
        catch (AggregateException ae)
        {
            ae.Handle(e =>
            {
                Console.WriteLine(e);
                return true;
            });
        }
    }
}
