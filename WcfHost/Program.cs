using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace WcfHost
{
    class Program
    {
        internal static ServiceHost myServiceHost = null;

        internal static void StartService()
        {
            try
            {
                myServiceHost = new ServiceHost(typeof(RoomReservationServices.RoomReservationService), new Uri[] { new Uri("http://localhost:9000/RoomReservation") });
                //myServiceHost.Description.Behaviors.Add(new ServiceMetadataBehavior() { HttpGetEnabled = true });
                myServiceHost.Open();

            }
            catch (AddressAccessDeniedException ex)
            {
                Console.WriteLine("either start Visual Studio in elevated admin " +
                    "mode or register the listener port with netsh.exe");

            }
            catch (Exception ex)
            {

            }
        }

        internal static void StopService()
        {
            if (myServiceHost!=null&&myServiceHost.State==CommunicationState.Opened)
            {
                myServiceHost.Close();
            }
        }

        static void Main(string[] args)
        {
            StartService();
            Console.WriteLine("Server is running. Press return to exit");
            Console.ReadLine();
            StopService();
        }
    }
}
