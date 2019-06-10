using InternshipHMSModels.Core.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace InternshipHMSModels.Models
{
    public class RoomImages : ObjectModel
    {
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }       
        public Guid RoomID { get; set; }
        [ForeignKey("RoomID")]
        public virtual Room RoomImages_Room { get; set; }


    }
}
