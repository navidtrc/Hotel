using InternshipHMSModels.Core.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipHMSModels.Models
{
    public class Reservation : ObjectModel
    {
        [Index("UK_Reservation_Number", IsUnique = true)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Number { get; set; }
        public Guid ReservationStateID { get; set; }
        [ForeignKey("ReservationStateID")]
        public virtual ReservationState Reservation_ReservationState { get; set; }
        public Guid CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public virtual Customer Reservation_Customer { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public virtual ICollection<ReservationRooms> Reservation_ReservationRooms_List { get; set; }
        public virtual ICollection<Checking> Reservation_Checking_List { get; set; }



    }
}
