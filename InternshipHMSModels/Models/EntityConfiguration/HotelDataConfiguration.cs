using InternshipHMSModels.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipHMSModels.Models.EntityConfiguration
{
    public class HotelDataConfiguration : EntityTypeConfiguration<HotelData>
    {
        public HotelDataConfiguration()
        {
            this.HasMany(m => m.HotelData_Room_List).WithRequired(r => r.Room_HotelData);
            this.ToTable("HotelData", "Basic");
        }
    }
}
