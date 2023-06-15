using Xunit;

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace S02_DataSharingSynchronization;

public partial class S02_03_SpinLockingLockRecursion
{
    // true = exception, false = deadlock
    static SpinLock sl = new SpinLock(true);

    private void Recurse(int x)
    {
        // lock recursion is being able to take the same lock multiple times
        bool lockTaken = false;
        try
        {
            sl.Enter(ref lockTaken);
        }
        catch (LockRecursionException e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
        finally
        {
            if (lockTaken)
            {
                Console.WriteLine($"Took a lock, x = {x}.");
                Recurse(x - 1);
                sl.Exit();
            }
            else
            {
                Console.WriteLine($"Failed to take a lock, x = {x}");
            }
        }
    }

    [Fact]
    public void LockRecursion()
    {
        Recurse(5);
        Console.WriteLine("All done here.");
    }
}
