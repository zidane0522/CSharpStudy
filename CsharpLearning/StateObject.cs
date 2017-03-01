using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpLearning
{
    class StateObject
    {
        private int state = 5;
        public void ChangeState(int loop)
        {
            try
            {
            if (state==5)
            {
                state++;
                Trace.Assert(state == 6, "Race condition occurred after" + loop + " loops");
            }
            state = 5;
            }
            catch (Exception ex)
            {

            }

        }
    }

    public class SampleTask
    {
        public void RaceCondition(object o)
        {
            try
            {
                Trace.Assert(o is StateObject, "o must be of type StateObject");
                StateObject state = o as StateObject;
                int i = 0;
                while (true)
                {
                    lock (state)
                    {
                        state.ChangeState(i++);
                    }

                }
            }
            catch (Exception ex)
            {

            }

        }
    }
}
