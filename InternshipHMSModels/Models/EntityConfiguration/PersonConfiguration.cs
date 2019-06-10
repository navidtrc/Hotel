using InternshipHMSModels.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipHMSModels.Models.EntityConfiguration
{
    public class PersonConfiguration : EntityTypeConfiguration<Person>
    {
        public PersonConfiguration()
        {
            this.ToTable("Person", "Basic");
            this.HasOptional(r => r.Person_ApplicationUser).WithRequired(w => w.AplicationUser_Person);
            this.HasMany(m => m.ContactInfo).WithRequired(r => r.ContactInformation_Person);
        }
    }
}
