using InternshipHMSModels.Core.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipHMSModels.Models
{
    public class Checking : ObjectModel
    {
        public Guid RoomID { get; set; }
        [ForeignKey("RoomID")]
        public virtual Room Checking_Room { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public Guid? ReservationID { get; set; }
        public virtual Reservation Checking_Reservation { get; set; }
        public virtual ICollection<Passenger> Checking_Passenger_List { get; set; }
    }
}
