using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CsharpLearning
{
    class ThreadPoolCls
    {
        public ThreadPoolCls()
        {
            int nWorkerThreads;
            int nCompletionPortThreads;
            ThreadPool.GetMaxThreads(out nWorkerThreads,out nCompletionPortThreads);

            Console.WriteLine("Max worker threads:{0}," +
                "IO completion threads:{1}", nWorkerThreads, nCompletionPortThreads);
            for (int i = 0; i < 5; i++)
            {
                ThreadPool.QueueUserWorkItem(JobForThread);
            }
            Thread.Sleep(3000);

        }

        static void JobForThread(object state)
        {
            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(1);

                Console.WriteLine("loop {0},running inside pooled thread{1}",i,Thread.CurrentThread.ManagedThreadId);
            }
        }
    }
}
