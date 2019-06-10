using InternshipHMSModels.Core.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipHMSModels.Models
{
    public class PhoneType : ObjectModel
    {
        [Required]
        public string Title { get; set; }
        public virtual ICollection<ContactInformation> PhoneType_ContactInformation_List { get; set; }
    }
   
}
