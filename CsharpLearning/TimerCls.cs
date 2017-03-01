using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CsharpLearning
{
    class TimerCls
    {
        public TimerCls()
        {
            
        }

        public static void ThreadTimer()
        {
            var t1 = new System.Threading.Timer(TimeAction, null, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(3));
            Thread.Sleep(15000);
            t1.Dispose();
        }

        public static void TimeAction(object o)
        {
            Console.WriteLine("System.Threading.Timer {0:T}",DateTime.Now);
        }
    }
}
