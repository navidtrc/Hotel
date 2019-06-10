using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipHMSModels.Models
{
    public class Customer : Person
    {
        [Index("UK_Customer_CustomerCode",IsUnique =true)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CustomerCode { get; set; }
        public string  PassportNumber { get; set; }
        public Guid? ParentID { get; set; }
        public virtual ICollection<Reservation> Customer_Reservation_List { get; set; }
        public virtual ICollection<Passenger> Customer_Passenger_List { get; set; }
    }
    
}
