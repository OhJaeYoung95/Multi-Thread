using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerCore
{
    class Program
    {
        #region ReaderWriterLock
        // 1. 근성
        // 2. 양보
        // 3. 갑질

        // 상호배제
        // Monitor
        static object _RWlock = new object();
        static SpinLock _RWlock2 = new SpinLock();

        // RWLock ReaderWriterLock
        static ReaderWriterLockSlim _RWlock3 = new ReaderWriterLockSlim();


        class Reward
        {

        }

        // 99.999%
        static Reward GetRewardById(int id)
        {
            _RWlock3.EnterReadLock();

            _RWlock3.ExitReadLock();
            lock (_lock)
            {

            }
            return null;
        }

        // 0.001%
        static void AddReward(Reward reward)
        {
            _RWlock3.EnterWriteLock();

            _RWlock3.ExitWriteLock();
            lock (_lock)
            {

            }
        }
        #endregion

        #region ReaderWriterLock Practice
        static volatile int count = 0;
        static Lock _lock = new Lock();

        static void ReaderWriterLockPractice()
        {
            Task t1 = new Task(delegate ()
            {
                for (int i = 0; i < 100000; i++)
                {
                    _lock.WriteLock();
                    _lock.WriteLock();
                    count++;
                    _lock.WriteUnlock();
                    _lock.WriteUnlock();
                }
            });
            Task t2 = new Task(delegate ()
            {
                for (int i = 0; i < 100000; i++)
                {
                    _lock.WriteLock();
                    count--;
                    _lock.WriteUnlock();
                }
            });

            t1.Start();
            t2.Start();

            Task.WaitAll(t1, t2);

            Console.WriteLine(count);
        }
        #endregion

        #region Thread Local Storage

        static ThreadLocal<string> ThreadName = new ThreadLocal<string>(() => { return $"My Name Is {Thread.CurrentThread.ManagedThreadId}"; });

        static void WhoAmI()
        {
            bool repeat = ThreadName.IsValueCreated;

            if(repeat)
                Console.WriteLine(ThreadName.Value + "(repeat)");
            else
                Console.WriteLine(ThreadName.Value);
        }

        #endregion
        static void Main(string[] args)
        {
            ThreadPool.SetMinThreads(1, 1);
            ThreadPool.SetMaxThreads(3, 3);
            Parallel.Invoke(WhoAmI, WhoAmI, WhoAmI, WhoAmI, WhoAmI, WhoAmI, WhoAmI, WhoAmI);
        }
    }
}
