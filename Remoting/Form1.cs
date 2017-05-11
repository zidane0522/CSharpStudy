using RemotingLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Activation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Remoting
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //ClientActivated();
            //ServerSingleton();
            ServerSingleCall();

            RunTest("CAO1");
            RunTest("CAO2");
        }

        private static void RunTest(string objectName)
        {
            DemoClass obj = new DemoClass();
            obj.ShowAppDomain();
            obj.ShowCount(objectName);
            Console.WriteLine("{0},the count is {1}.", objectName, obj.GetCount());
            obj.ShowCount(objectName);
            Console.WriteLine("{0},the count is {1}.", objectName, obj.GetCount());
        }



        /// <summary>
        /// 注册客户端激活
        /// </summary>
        private static void ClientActivated()
        {
            Type t = typeof(DemoClass);
            string url = "tcp://127.0.0.1:8501";
            RemotingConfiguration.RegisterActivatedClientType(t,url);
        }

        /// <summary>
        /// 注册服务端激活单次调用
        /// </summary>
        private static void ServerSingleCall()
        {
            Type t = typeof(DemoClass);
            string url = "tcp://127.0.0.1:8501/SimpleRemote/ServerSingleCall";
            RemotingConfiguration.RegisterWellKnownClientType(t,url);
        }

        /// <summary>
        /// 注册服务端激活单例
        /// </summary>
        private static void ServerSingleton()
        {
            Type t = typeof(DemoClass);
            string url = "tcp://127.0.0.1:8501/SimpleRemote/ServerSingleton";
            RemotingConfiguration.RegisterWellKnownClientType(t, url);
        }

        /// <summary>
        /// 免注册直接激活,只能创建客户激活对象
        /// </summary>
        private static void NoConfigActivation()
        {
            string url = "tcp://127.0.0.1:8501/SimpleRemote/";
            string url2 = "tcp://127.0.0.1:8501/SimpleRemote/ServerSingleCall";
            string url3 = "tcp://127.0.0.1:8501/SimpleRemote/ServerSingleton";
            object[] activationAtt = { new UrlAttribute(url) };
            DemoClass obj = (DemoClass)Activator.CreateInstance(typeof(DemoClass),null,activationAtt);
            DemoClass obj2 = (DemoClass)RemotingServices.Connect(typeof(DemoClass),url2);
            DemoClass obj3 = (DemoClass)Activator.GetObject(typeof(DemoClass), url3);
        }
    }
}
