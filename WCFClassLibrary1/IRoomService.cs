using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCFClassLibrary1
{
    [ServiceContract(Namespace ="http://www.cninnovation.com/RoomReservation/2012")]
    public interface IRoomService
    {
        [OperationContract]
        bool ReserveRoom(RoomReservation roomReservation);

        [OperationContract]
        RoomReservation[] GetRoomReservations(DateTime fromTime, DateTime toTime);
    }
}
