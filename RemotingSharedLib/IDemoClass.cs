
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemotingSharedLib
{
    /// <summary>
    /// Remoting服务端与客户端
    /// </summary>
    public interface IDemoClass
    {

        void ShowCount(string name);//测试远程对象传递参数，以及对象状态的保存

        void ShowAppDomain();//验证远程对象所在的应用程序域

        DemoCount GetNewCount();

        int GetCount();//获取远程对象的返回值
    }

    [Serializable]
    public struct DemoCount
    {
        private readonly int count;
        public DemoCount(int count)
        {
            this.count = count;
        }

        public int Count
        {
            get { return count; }
        }

        public void ShowAppDomain()
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            Console.WriteLine(currentDomain.FriendlyName);
        }
    }
}
