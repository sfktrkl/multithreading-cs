using Xunit;

using System;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Linq;

namespace S03_ConcurrentCollections;

public partial class S03_03_ConcurrentStack
{
    private ConcurrentStack<int> stack = new ConcurrentStack<int>();

    [Fact]
    public void ConcurrentStack()
    {
        stack.Push(1);
        stack.Push(2);
        stack.Push(3);
        stack.Push(4);

        Task.Factory.StartNew(() =>
        {
            int result;
            if (stack.TryPeek(out result))
                Console.WriteLine($"{result} is on top");
        });

        Task.Factory.StartNew(() =>
        {
            int result;
            if (stack.TryPop(out result))
                Console.WriteLine($"Popped {result}");
        });

        Task.Factory.StartNew(() =>
        {
            var items = new int[5];
            if (stack.TryPopRange(items, 0, 5) > 0) // actually pops only 3 items
            {
                var text = string.Join(", ", items.Select(i => i.ToString()));
                Console.WriteLine($"Popped these items: {text}");
            }
        });
    }
}
