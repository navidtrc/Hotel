using InternshipHMSModels.Core.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipHMSModels.Models
{
    public class Passenger : ObjectModel
    {
        public Guid CheckingID { get; set; }
        [ForeignKey("CheckingID")]
        public virtual Checking Passenger_Checking { get; set; }
        public Guid CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public virtual Customer Passenger_Customer { get; set; }
    }
}
