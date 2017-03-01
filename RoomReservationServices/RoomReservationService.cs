using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFClassLibrary1;

namespace RoomReservationServices
{
    public class RoomReservationService : IRoomService
    {
        public RoomReservation[] GetRoomReservations(DateTime fromTime, DateTime toTime)
        {
            var data = new RoomReservationData.RoomReservationData();
            return data.GetReservations(fromTime, toTime);
        }

        public bool ReserveRoom(RoomReservation roomReservation)
        {
            var data = new RoomReservationData.RoomReservationData();
            data.ReserveRoom(roomReservation);
            return true;
        }
    }
}
