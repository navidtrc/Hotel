using InternshipHMSModels.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipHMSModels.Models.EntityConfiguration
{
    public class RoomConfiguration : EntityTypeConfiguration<Room>
    {
        public RoomConfiguration()
        {
            this.ToTable("Room", "Basic");
            this.HasMany(m => m.Room_RoomImages_List).WithRequired(o => o.RoomImages_Room);
            this.HasMany(m => m.Room_ReservationRooms_List)
                .WithRequired(r => r.ReservationRooms_Room);
            this.HasMany(m => m.Room_Checking_List)
                .WithRequired(r => r.Checking_Room);
            this.HasMany(m => m.Room_RoomPrice_List).WithRequired(r => r.RoomPrice_Room);
            this.HasOptional(o => o.Room_Facilities).WithMany(m => m.Facilities_Room_List);
        }
    }
}
