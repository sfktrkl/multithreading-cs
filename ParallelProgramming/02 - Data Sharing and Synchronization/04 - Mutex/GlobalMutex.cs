using Xunit;

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace S02_DataSharingSynchronization;

public partial class S02_04_Mutex
{
    private static void CreateMutex()
    {
        const string appName = "MyApp";
        Mutex mutex;
        try
        {
            mutex = Mutex.OpenExisting(appName);
            Console.WriteLine($"Sorry, {appName} is already running.");
            return;
        }
        catch (WaitHandleCannotBeOpenedException)
        {
            Console.WriteLine("We can run the program just fine.");
            // first arg = whether to give current thread initial ownership
            mutex = new Mutex(false, appName);
        }

        Thread.Sleep(1000);
    }

    [Fact]
    public void GlobalMutex()
    {
        CreateMutex();
    }

    [Fact]
    public void GlobalMutex2()
    {
        CreateMutex();
    }
}
