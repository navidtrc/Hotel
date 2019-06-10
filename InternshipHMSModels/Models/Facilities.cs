using InternshipHMSModels.Core.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipHMSModels.Models
{
    public class Facilities : ObjectModel
    {
        public int SingleBed { get; set; }
        public int DoubleBed { get; set; }
        public int Space { get; set; }
        public int Capacity { get; set; }
        public bool Kitchen { get; set; }
        public bool Terrace { get; set; }
        public bool TV { get; set; }
        public bool WiFi { get; set; }
        public bool SafeBox { get; set; }
        public bool GamingConsole { get; set; }
        public virtual ICollection<Room> Facilities_Room_List { get; set; }
    }
    
}
