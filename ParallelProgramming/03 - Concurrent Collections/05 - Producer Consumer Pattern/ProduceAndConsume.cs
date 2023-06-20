using Xunit;

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace S03_ConcurrentCollections;

public partial class S03_05_ProducerConsumerPattern
{
    static BlockingCollection<int> messages = new BlockingCollection<int>(
        new ConcurrentBag<int>(),
        10 /* bounded */
    );

    static CancellationTokenSource cts = new CancellationTokenSource();

    private static Random random = new Random();

    private static void RunConsumer()
    {
        foreach (var item in messages.GetConsumingEnumerable())
        {
            cts.Token.ThrowIfCancellationRequested();
            Console.WriteLine($"-{item}");
            Thread.Sleep(100);
        }
    }

    private static void RunProducer()
    {
        while (true)
        {
            cts.Token.ThrowIfCancellationRequested();
            int i = random.Next(100);
            messages.Add(i);
            Console.WriteLine($"+{i}\t");
        }
    }

    [Fact]
    public void ProduceAndConsume()
    {
        var producer = Task.Factory.StartNew(RunProducer, cts.Token);
        var consumer = Task.Factory.StartNew(RunConsumer, cts.Token);

        Thread.Sleep(2000);
        cts.Cancel();
    }
}
