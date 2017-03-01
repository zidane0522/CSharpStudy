using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CsharpLearning
{
    class ThreadCls
    {
        public ThreadCls()
        {
            var t1 = new Thread(ThreadMain);
            t1.Start();
            Console.WriteLine("This is the main thread.");
        }

        public void ThreadMain()
        {
            Console.WriteLine("Running in a thread.");
        }
    }
}
