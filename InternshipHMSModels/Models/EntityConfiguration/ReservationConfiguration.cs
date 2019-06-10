using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipHMSModels.Models.EntityConfiguration
{
    public class ReservationConfiguration : EntityTypeConfiguration<Reservation>
    {
        public ReservationConfiguration()
        {
            ToTable("Reservation", "Basic");
            this.HasMany(m => m.Reservation_ReservationRooms_List)
                .WithRequired(r => r.ReservationRooms_Reservation);
            this.HasMany(m => m.Reservation_Checking_List)
                .WithOptional(r => r.Checking_Reservation);

        }
    }
}
