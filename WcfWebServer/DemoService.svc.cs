using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace WcfWebServer
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“DemoService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 DemoService.svc 或 DemoService.svc.cs，然后开始调试。
    public class DemoService : IDemoServices
    {
        public async Task StartSendingMessage()
        {
            IDemoCallback callback = OperationContext.Current.GetCallbackChannel<IDemoCallback>();
            int loop = 0;
            while ((callback as IChannel).State == CommunicationState.Opened)
            {
                await callback.SendMessage(string.Format("Hello from the server{0}", loop++));
               
                await Task.Delay(5000);
            }
        }
    }

    public class RouteService : IRouteService
    {
        public static string Server { get; set; }

        public string GetData(string value)
        {
            string message = string.Format("Message from (0),You entered:{1}", Server, value);
            Console.WriteLine(message);
            return message;
        }
    }
}
