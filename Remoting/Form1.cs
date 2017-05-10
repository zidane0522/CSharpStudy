using RemotingLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting;
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
        }

        /// <summary>
        /// 注册客户端激活
        /// </summary>
        private static void ClientActivated()
        {
            Type t = typeof(DemoClass);
            string url = "tcp://127.0.0.1:8051";
            RemotingConfiguration.RegisterActivatedClientType(t,url);
        }
    }
}
