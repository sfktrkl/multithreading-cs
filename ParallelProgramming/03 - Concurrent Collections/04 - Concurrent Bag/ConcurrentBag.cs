using Xunit;

using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace S03_ConcurrentCollections;

public partial class S03_04_ConcurrentBag
{
    private ConcurrentBag<int> stack = new ConcurrentBag<int>();

    [Fact]
    public void ConcurrentBag()
    {
        // stack is LIFO
        // queue is FIFO
        // concurrent bag provides NO ordering guarantees

        // keeps a separate list of items for each thread typically requires no
        // synchronization, unless a thread tries to remove an item while the
        // thread-local bag is empty (item stealing)
        var bag = new ConcurrentBag<int>();
        var tasks = new List<Task>();
        for (int i = 0; i < 10; i++)
        {
            var i1 = i;
            tasks.Add(
                Task.Factory.StartNew(() =>
                {
                    bag.Add(i1);
                    Console.WriteLine($"{Task.CurrentId} has added {i1}");
                    int result;
                    if (bag.TryPeek(out result))
                        Console.WriteLine($"{Task.CurrentId} has peeked the value {result}");
                })
            );
        }

        Task.WaitAll(tasks.ToArray());

        // take whatever's last
        int last;
        if (bag.TryTake(out last))
            Console.WriteLine($"Last element is {last}");
    }
}
