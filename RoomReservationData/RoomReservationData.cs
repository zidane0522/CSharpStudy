using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFClassLibrary1;

namespace RoomReservationData
{
    public class RoomReservationData
    {
        public void ReserveRoom(RoomReservation roomReservation)
        {
            try
            {
                using (var data = new RoomReservationContext())
                {
                    data.RoomReservations.Add(roomReservation);
                    data.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }

        }

        public RoomReservation[] GetReservations(DateTime fromTime, DateTime toTime)
        {
            using (var data=new RoomReservationContext())
            {
                return (from r in data.RoomReservations where
                        r.StartTime > fromTime && r.EndTime < toTime
                        select r).ToArray();
            }
        }
    }
}
