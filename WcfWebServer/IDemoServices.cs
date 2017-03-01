using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WcfWebServer
{
    [ServiceContract(CallbackContract = typeof(IDemoCallback))]
    interface IDemoServices
    {
        [OperationContract]
        Task StartSendingMessage();
    }

    [ServiceContract]
    interface IDemoCallback
    {
        [OperationContract(IsOneWay = true)]
        Task SendMessage(string message);
    }

    [ServiceContract(Namespace ="http://www.cninnovation.com/Services/2012")]
    interface IRouteService
    {
        [OperationContract]
        string GetData(string value);
    }

}
