using InternshipHMSModels.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipHMSModels.Models.EntityConfiguration
{
    public class ContactInformationConfiguration : EntityTypeConfiguration<ContactInformation>
    {
        public ContactInformationConfiguration()
        {
            this.ToTable("ContactInformation", "Basic");
        }
    }
}
