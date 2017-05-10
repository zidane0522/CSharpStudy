using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemotingLib
{
    public class DemoClass:MarshalByRefObject//封送
    {
        private int count = 0;
        public DemoClass()//追踪远程对象创建的时机
        {
            Console.WriteLine("\n----DemoClass Constructor----");
        }

        public void ShowCount(string name)//测试远程对象传递参数，以及对象状态的保存
        {
            count++;
            Console.WriteLine("{0},the count is {1}.", name, count);
        }

        public void ShowAppDomain()//验证远程对象所在的应用程序域
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            Console.WriteLine(currentDomain.FriendlyName);
        }

        public int GetCount()//获取远程对象的返回值
        { return count; }
    }
}
