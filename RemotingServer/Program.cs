using RemotingLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using System.Threading.Tasks;

namespace RemotingServer
{
    class Program
    {
        static void Main(string[] args)
        {
            RegisterChannel();
            RegisterChannel2();
            RemotingConfiguration.ApplicationName = "SimpleRemote";
            ClientActivated();
            ServerActivatedSingleton();
            ServerActivateSingleCall();
            Console.WriteLine("服务开启，按任意键退出……");
            Console.Read();
        }

        /// <summary>
        /// 注册通道
        /// </summary>
        private static void RegisterChannel()
        {
            IChannelReceiver tcpChnl = new TcpChannel(8501);
            ChannelServices.RegisterChannel(tcpChnl,false);

            IChannel httpChnl = new HttpChannel(8502);
            ChannelServices.RegisterChannel(httpChnl, false);
       
        }

        /// <summary>
        /// 自定义Formatter和通道名称的注册方式
        /// </summary>
        private static void RegisterChannel2()
        {
            IServerChannelSinkProvider formatter;
            formatter = new BinaryServerFormatterSinkProvider();
            IDictionary propertyDic = new Hashtable();
            propertyDic["name"] = "CustomTcp";
            propertyDic["port"] = 8503;
            IChannel tcpChnl = new TcpChannel(propertyDic,null,formatter);
            ChannelServices.RegisterChannel(tcpChnl, false);
        }

        /// <summary>
        /// 客户端激活方法
        /// </summary>
        private static void ClientActivated()
        {
            Console.WriteLine("方式：client activated object,客户端激活");
            Type t = typeof(DemoClass);
            RemotingConfiguration.RegisterActivatedServiceType(t);
        }

        /// <summary>
        /// 服务端单次调用激活
        /// </summary>
        private static void ServerActivateSingleCall()
        {
            Console.WriteLine("方式：服务端单次调用激活");
            Type t = typeof(DemoClass);
            RemotingConfiguration.RegisterWellKnownServiceType(t,"ServerSingleCall",WellKnownObjectMode.SingleCall);
        }

        /// <summary>
        /// 服务器端单例模式激活
        /// </summary>
        private static void ServerActivatedSingleton()
        {
            Console.WriteLine("方式：单例模式激活");
            Type t = typeof(DemoClass);
            RemotingConfiguration.RegisterWellKnownServiceType(t, "ServerSingleton", WellKnownObjectMode.Singleton);
        }
    }
}
