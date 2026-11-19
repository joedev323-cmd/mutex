using System;
using System.Threading;

namespace muttst;

class BankAtm
{
    static Mutex mutex = new Mutex();

   public static void UseAtm()
    {
        mutex.WaitOne();
        try
        {
            Console.WriteLine(Thread.CurrentThread.Name + " wozza");
            Thread.Sleep(2000);
            Console.WriteLine(Thread.CurrentThread.Name + " has finished");
        }
        finally
        {
            mutex.ReleaseMutex();
        }
    }
}

class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("hello mutex testing");

        var t1 = new Thread(() => { Thread.CurrentThread.Name = "T1"; BankAtm.UseAtm(); });
        var t2 = new Thread(() => { Thread.CurrentThread.Name = "T2"; BankAtm.UseAtm(); });

        t1.Start();
        t2.Start();

        t1.Join();
        t2.Join();
    }
}   