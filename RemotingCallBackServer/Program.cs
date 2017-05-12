using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RemotingCallBackServer
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    public delegate void NumberChangedEventHandler(string name,int count);

    public class Server : MarshalByRefObject
    {
        private int count = 0;
        private string serverName = "SimpleServer";
        public event NumberChangedEventHandler NumberChanged;
        //触发事件，调用客户端方法
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DoSomething()
        {
            count++;
            if (NumberChanged != null)
            {
                Delegate[] delArray = NumberChanged.GetInvocationList();
                foreach (Delegate del in delArray)
                {
                    NumberChangedEventHandler method;//=(NumberChangedEventHandler)
                    try
                    {
                        method(serverName,count);
                    }
                    catch (Exception)
                    {
                        Delegate.Remove(NumberChanged,del);
                    }
                }
            }
        }

        public void InvokeClient(Client remoteClient,int x,int y)
        {
            int total=remoteClient.add
        }
    }
}
