using InternshipHMSModels.Core.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipHMSModels.Models
{
    public enum ReserveState
    {
        Temporary,
        Fixed,
        PayUp,
        Canceled
    }
    public class ReservationState : ObjectModel
    {
        public ReserveState Title { get; set; }
        public virtual ICollection<Reservation> ReservationState_Reservation_List { get; set; }
    }
}
