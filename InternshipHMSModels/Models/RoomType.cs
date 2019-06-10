using InternshipHMSModels.Core.Entity.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipHMSModels.Models
{
    public class RoomType : ObjectModel
    {
        public string Type { get; set; }
        public virtual ICollection<Room> RoomType_Room_List { get; set; }
    }
   
}
