using InternshipHMSModels.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipHMSModels.Models.EntityConfiguration
{
    public class RoomImagesConfiguration : EntityTypeConfiguration<RoomImages>
    {
        public RoomImagesConfiguration()
        {
            this.ToTable("RoomImages", "Basic");
        }
    }
}
