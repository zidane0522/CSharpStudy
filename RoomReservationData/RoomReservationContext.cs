using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFClassLibrary1;

namespace RoomReservationData
{
    public class RoomReservationContext : DbContext
    {
        public RoomReservationContext() : base("RoomReservation")
        {

        }

        public DbSet<RoomReservation> RoomReservations { get; set; }
    }
}
