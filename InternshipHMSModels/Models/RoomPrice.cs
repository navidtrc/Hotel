using InternshipHMSModels.Core.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipHMSModels.Models
{
    public class RoomPrice : ObjectModel
    {
        public int Price { get; set; }
        public Guid RoomID { get; set; }
        [ForeignKey("RoomID")]
        public virtual Room RoomPrice_Room { get; set; }
        public DateTime Date { get; set; }
    }
}
