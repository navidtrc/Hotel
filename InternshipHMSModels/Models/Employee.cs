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
    public class Employee : Person
    {
        [Index("UK_Employee_EmployeeCode", IsUnique = true)]
        [Column(TypeName = "NVARCHAR")]
        [StringLength(8)]
        [Required]
        [Display(Name = "شماره پرسنلی")]
        public string EmployeeCode { get; set; }
        [NotMapped]
        public string RoleHelper { get; set; }
    }
   
}
