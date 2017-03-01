using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Routing;
using System.Text;
using System.Threading.Tasks;
using WcfWebCallbackClient.ServiceReference1;

namespace WcfWebCallbackClient
{
    

    class Program
    {
        private class CallbackHandler : IDemoServicesCallback
        {
            public void SendMessage(string message)
            {
                Console.WriteLine("message from the server {0}",message);
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("client wait for the server");
            Console.ReadLine();
            StartSendRequest();
            Console.WriteLine("next return to exit");
            Console.ReadLine();
        }

        static async void StartSendRequest()
        {
            var callbackInstance = new InstanceContext(new CallbackHandler());
            var client = new DemoServicesClient(callbackInstance);
            await client.StartSendingMessageAsync();
        }


    }
}
