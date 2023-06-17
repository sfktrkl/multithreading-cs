using Xunit;

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace S02_DataSharingSynchronization;

public partial class S02_05_ReaderWriterLock
{
    [Fact]
    public void UpgradableLock()
    {
        var padlock = new ReaderWriterLockSlim();

        int x = 0;
        var tasks = new List<Task>();
        for (int i = 0; i < 10; i++)
        {
            tasks.Add(
                Task.Factory.StartNew(() =>
                {
                    padlock.EnterUpgradeableReadLock();
                    Console.WriteLine($"Entered upgradable read lock, x = {x}");
                    padlock.EnterWriteLock();
                    x += 1;
                    padlock.ExitWriteLock();
                    Console.WriteLine($"Exited upgradable read lock, x = {x}");
                    padlock.ExitUpgradeableReadLock();
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
