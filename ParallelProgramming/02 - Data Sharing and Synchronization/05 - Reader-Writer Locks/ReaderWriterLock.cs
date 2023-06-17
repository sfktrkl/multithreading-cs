using Xunit;

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace S02_DataSharingSynchronization;

public partial class S02_05_ReaderWriterLock
{
    [Fact]
    public void ReaderWriterLock()
    {
        var padlock = new ReaderWriterLockSlim();

        int x = 0;
        var tasks = new List<Task>();
        for (int i = 0; i < 10; i++)
        {
            tasks.Add(
                Task.Factory.StartNew(() =>
                {
                    padlock.EnterReadLock();
                    Console.WriteLine($"Entered read lock, x = {x}");
                    padlock.ExitReadLock();
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
