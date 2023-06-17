using Xunit;

using System;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace S03_ConcurrentCollections;

public partial class S03_02_ConcurrentQueue
{
    private ConcurrentQueue<int> q = new ConcurrentQueue<int>();

    [Fact]
    public void ConcurrentQueue()
    {
        q.Enqueue(1);
        q.Enqueue(2);

        // Queue: 2 1 <- front
        Task.Factory.StartNew(() =>
        {
            int result;
            if (q.TryDequeue(out result))
                Console.WriteLine($"Removed element {result}");
        });

        Task.Factory.StartNew(() =>
        {
            int result;
            if (q.TryPeek(out result))
                Console.WriteLine($"Last element is {result}");
        });
    }
}
