using InternshipHMSModels.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipHMSModels.Models.EntityConfiguration
{
    public class PhoneTypeConfiguration : EntityTypeConfiguration<PhoneType>
    {
        public PhoneTypeConfiguration()
        {
            this.ToTable("PhoneType", "Basic");
            this.HasMany(m => m.PhoneType_ContactInformation_List).WithOptional(r => r.ContactInformation_PhoneType);
        }
    }
}
