using Xunit;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace S04_TaskCoordination;

public partial class S04_05_Semaphore
{
    [Fact]
    public void Semaphore()
    {
        var semaphore = new SemaphoreSlim(2, 10);

        for (int i = 0; i < 20; ++i)
        {
            Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"Entering task {Task.CurrentId}.");
                semaphore.Wait(); // ReleaseCount--
                Console.WriteLine($"Processing task {Task.CurrentId}.");
            });
        }

        while (semaphore.CurrentCount <= 2)
        {
            Console.WriteLine($"Semaphore count: {semaphore.CurrentCount}");
            Thread.Sleep(1000);
            semaphore.Release(2); // ReleaseCount += n
        }
    }
}
