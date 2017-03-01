using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CsharpLearning
{
    class TcpReceiveCls
    {
        public TcpReceiveCls()
        {
            Thread thread = new Thread(new ThreadStart(this.Listen));
            thread.Start();
        }

        public void Listen()
        {
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            Int32 port = 2112;
            TcpListener tcpListener = new TcpListener(localAddr, port);
            tcpListener.Start();
            TcpClient tcpClient = tcpListener.AcceptTcpClient();
            NetworkStream ns = tcpClient.GetStream();
            StreamReader sr = new StreamReader(ns);
            string result = sr.ReadToEnd();
        }

    }
}
