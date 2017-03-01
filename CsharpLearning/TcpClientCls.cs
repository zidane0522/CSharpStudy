using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CsharpLearning
{
    class TcpClientCls
    {
        public TcpClientCls()
        {
            TcpClient tcpclient = new TcpClient("127.0.0.1", 8081);
            NetworkStream ns = tcpclient.GetStream();
            FileStream fs = File.Open("TimerCls.cs", FileMode.Open);
            int data = fs.ReadByte();
            while (data != -1)
            {
                ns.WriteByte((byte)data);
                data = fs.ReadByte();
            }
            fs.Close();
            ns.Close();
            tcpclient.Close();
        }
    }
}
