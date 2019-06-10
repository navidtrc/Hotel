using InternshipHMSModels.Core.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipHMSModels.Models
{
    public class ContactInformation : ObjectModel
    {
        public string PhoneNumber { get; set; }
        public Guid? PhoneTypeID { get; set; }
        [ForeignKey("PhoneTypeID")]
        public virtual PhoneType ContactInformation_PhoneType { get; set; }
        [NotMapped]
        public string Type { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public Guid PersonID { get; set; }
        [ForeignKey("PersonID")]
        public virtual Person ContactInformation_Person { get; set; }

    }
}
