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
        // 1. 근성
        // 2. 양보
        // 3. 갑질

        // 상호배제
        // Monitor
        static object _lock = new object();
        static SpinLock _lock2 = new SpinLock();

        // RWLock ReaderWriterLock
        static ReaderWriterLockSlim _lock3 = new ReaderWriterLockSlim();


        class Reward
        {

        }

        // 99.999%
        static Reward GetRewardById(int id)
        {
            _lock3.EnterReadLock();

            _lock3.ExitReadLock();
            lock (_lock)
            {

            }
            return null;
        }

        // 0.001%
        static void AddReward(Reward reward)
        {
            _lock3.EnterWriteLock();

            _lock3.ExitWriteLock();
            lock (_lock)
            {

            }
        }

        static void Main(string[] args)
        {
            lock (_lock)
            {

            }            
        }
    }
}
