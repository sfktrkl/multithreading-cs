using Xunit;

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace S02_DataSharingSynchronization;

public partial class S02_04_Mutex
{
    class BankAccount
    {
        public int Balance { get; private set; }

        public BankAccount(int balance)
        {
            Balance = balance;
        }

        public void Deposit(int amount)
        {
            Balance += amount;
        }

        public void Withdraw(int amount)
        {
            Balance -= amount;
        }

        public void Transfer(BankAccount where, int amount)
        {
            where.Balance += amount;
            Balance -= amount;
        }
    }

    [Fact]
    public void LocalMutex()
    {
        var tasks = new List<Task>();
        var ba = new BankAccount(0);
        var ba2 = new BankAccount(0);

        // many synchro types deriving from WaitHandle
        // Mutex = mutual exclusion

        // two types of mutexes
        // this is a _local_ mutex
        Mutex mutex = new Mutex();
        Mutex mutex2 = new Mutex();

        for (int i = 0; i < 10; ++i)
        {
            tasks.Add(
                Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; ++j)
                    {
                        bool haveLock = mutex.WaitOne();
                        try
                        {
                            ba.Deposit(1); // deposit 10000 overall
                        }
                        finally
                        {
                            if (haveLock)
                                mutex.ReleaseMutex();
                        }
                    }
                })
            );
            tasks.Add(
                Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; ++j)
                    {
                        bool haveLock = mutex2.WaitOne();
                        try
                        {
                            ba2.Deposit(1); // deposit 10000
                        }
                        finally
                        {
                            if (haveLock)
                                mutex2.ReleaseMutex();
                        }
                    }
                })
            );

            // transfer needs to lock both accounts
            tasks.Add(
                Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        bool haveLock = Mutex.WaitAll(new[] { mutex, mutex2 });
                        try
                        {
                            ba.Transfer(ba2, 1); // transfer 10k from ba to ba2
                        }
                        finally
                        {
                            if (haveLock)
                            {
                                mutex.ReleaseMutex();
                                mutex2.ReleaseMutex();
                            }
                        }
                    }
                })
            );
        }

        Task.WaitAll(tasks.ToArray());

        Console.WriteLine($"Final balance is: ba={ba.Balance}, ba2={ba2.Balance}.");
    }
}
