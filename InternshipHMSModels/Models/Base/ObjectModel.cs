using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipHMSModels.Core.Entity.Base
{
    public abstract class ObjectModel
    {
        [Key]
        public Guid ID { get; set; }
        public DateTime CreateDate { get; set; }
        public ObjectModel()
        {
            ID = Guid.NewGuid();
            CreateDate = DateTime.Now;
        }
    }
}
