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
    class Program
    {
        public delegate string GetAStrign();
        static void Main(string[] args)
        {
            //int fds=09;
            //GetAStrign fd = fds.ToString;
            //fd.Invoke();

            //int x = 40;

            //GetAStrign fistMethod = x.ToString;
            //Currency balance = new Currency(34, 50);
            //fistMethod = balance.ToString;


            //fistMethod = new GetAStrign(balance.fdsfdsfdsf);

            //Employee[] employees =
            //    {
            //    new Employee("AAA",20000),
            //    new Employee("BBB", 10000),
            //    new Employee("CCCC",25042),
            //    new Employee("DDDD",454354),
            //    new Employee("EEEE",3432456),
            //    new Employee("FFFF",4345)
            //};

            //BubbleSorter.Sort<Employee>(employees, Employee.CompareSalary);

            //var dealer = new CarDealer();
            //var michael = new Consumer("Micheal");
            //dealer.NewCarInfo += michael.NewCarIsHere;
            //dealer.NewCar("Ferrari");

            //var jack = new Consumer("Jack");
            //dealer.NewCarInfo += jack.NewCarIsHere;
            //dealer.NewCar("Mercedes");

            //dealer.NewCarInfo -= michael.NewCarIsHere;//结束后取消订阅，避免资源泄露
            //dealer.NewCar("Red Bull Racing");


            //ParallelLoopResult result = Parallel.For(10, 40, async(int i,ParallelLoopState pls) =>
            // {
            //     Console.WriteLine("{0},task:{1},thread:{2}", i, Task.CurrentId, Thread.CurrentThread.ManagedThreadId);

            //     await Task.Delay(1);//异步等待
            //     if (i>15)
            //     {
            //         pls.Break();
            //     }
            // });
            //Console.WriteLine("Is completed:{0}", result.IsCompleted);
            //Console.WriteLine("lowest break iteration：{0}", result.LowestBreakIteration);

            //ThreadPoolCls df = new ThreadPoolCls();

            //ThreadCls thd = new ThreadCls();

            //var state = new StateObject();
            //for (int i = 0; i < 2; i++)
            //{
            //    Task.Run(() => new SampleTask().RaceCondition(state));
            //}

            ////ThreadCls thd = new ThreadCls();
            //TimerCls.ThreadTimer();

            //HttpClientCls.GetData();

            //string url = "http://tcc.taobao.com/cc/json/mobile_tel_segment.htm?tel=13837377355" ;
            //WebRequest wrt = WebRequest.Create(url);
            //WebResponse wrse = wrt.GetResponse();
            //Stream strm = wrse.GetResponseStream();
            //StreamReader sr = new StreamReader(strm, Encoding.Default);
            //string html = sr.ReadToEnd();
            //String[] d = html.Split(new String[] { "province:'" }, StringSplitOptions.None);
            //string p = d[1].Split('\'').First();
            //string s3 = "巅峰时代SDFsdfd";
            //System.Text.RegularExpressions.Regex reg1 = new System.Text.RegularExpressions.Regex("^[a-zA-Z]");
            //bool d = reg1.IsMatch(s3);

            //string sysfilegign = MD5FileSign.getMD5Hash(@"C:\Users\Administrator\Desktop\download.jpg");
            //string confilesign = MD5FileSign.getMD5Hash(@"‪C:\Users\Administrator\Desktop\2c9088d355.jpg");
            //string res = sysfilegign == confilesign ? "suc" : "fail";
            //MD5FileSign.savebaseImg();

            //string dd = "kl  图形df 图形 ";
            //string[] df = dd.Split(' ');
            //string dds = "";
            //for (int i = 0; i < df.Length; i++)
            //{
            //    dds += df[i];
            //}
            //string fdf = dds.Replace("图形", " 图形 ");
            //string dsfdsf = fdf.Trim();
            DateTime dd = DateTime.Now.Date;
            DateTime ww = DateTime.Now.AddDays(5).Date;
            int dsf = ww.Subtract(dd).Days;
            Console.ReadKey();
        }
    }
}
