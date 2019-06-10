using InternshipHMSModels.Core.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipHMSModels.Models
{
    public class HotelData : ObjectModel
    {
        [Required]
        public string Name { get; set; }
        public int Rate { get; set; }
        public virtual ICollection<Room> HotelData_Room_List { get; set; }
    }

}
