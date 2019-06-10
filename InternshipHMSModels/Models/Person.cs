using InternshipHMSModels.Core.Entity.Base;
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
    public class Person : ObjectModel
    {
        [Display(Name = "نام")]
        [Required]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }
        [Index("UK_Person_NationalCode", IsUnique = true)]
        [Column(TypeName = "NVARCHAR")]
        [StringLength(10)]
        [Required]
        [Display(Name = "کد ملی")]
        public string NationalCode { get; set; }
        [Display(Name = "تولد")]
        public DateTime? Birthdate { get; set; }
        [Display(Name = "ملیت")]
        public string Nationality { get; set; }
        [Display(Name = "محل زندگی")]
        public string LivingPlace { get; set; }
        public virtual ICollection<ContactInformation> ContactInfo { get; set; }
        public virtual ApplicationUser Person_ApplicationUser { get; set; }
        [NotMapped]
        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
        }

    }
  
}
