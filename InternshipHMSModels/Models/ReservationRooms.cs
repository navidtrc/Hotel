using InternshipHMSModels.Core.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipHMSModels.Models
{
    public class ReservationRooms : ObjectModel
    {
        public Guid ReservationID { get; set; }
        [ForeignKey("ReservationID")]
        public virtual Reservation ReservationRooms_Reservation { get; set; }

        public Guid RoomID { get; set; }
        [ForeignKey("RoomID")]
        public virtual Room ReservationRooms_Room { get; set; }
    }
}
