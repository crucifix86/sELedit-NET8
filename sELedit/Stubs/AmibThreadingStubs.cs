using System;
using System.Threading;
using System.Threading.Tasks;

namespace Amib.Threading
{
    public class SmartThreadPool : IDisposable
    {
        private readonly int _maxWorkerThreads;
        
        public SmartThreadPool(int idleTimeout, int maxWorkerThreads)
        {
            _maxWorkerThreads = maxWorkerThreads;
        }

        public void QueueWorkItem(Action<object> action, object state)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(o => action(o)), state);
        }

        public void Dispose()
        {
            // Cleanup if needed
        }
    }
}