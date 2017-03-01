using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Routing;
using System.Text;
using System.Threading.Tasks;

namespace Router
{
    class Program
    {
        internal static ServiceHost routerHost = null;
        internal static void StartService()
        {
            try
            {
                routerHost = new ServiceHost(typeof(RoutingService));
                routerHost.Faulted += RouterHost_Faulted;
                routerHost.Open();
            }
            catch (AddressAccessDeniedException)
            {
                Console.WriteLine("either start Visual Studio in elevated admin " + "mode or register the lister port with netsh.exe");
            }
        }

        private static void RouterHost_Faulted(object sender, EventArgs e)
        {
            Console.WriteLine("router faulted");
        }

        internal static void StopService()
        {
            if (routerHost != null && routerHost.State == CommunicationState.Opened)
            {
                routerHost.Close();
            }
        }
        static void Main(string[] args)
        {
        }
    }
}
