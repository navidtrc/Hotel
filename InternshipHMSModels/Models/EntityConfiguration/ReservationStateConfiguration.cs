using InternshipHMSModels.Core.Entity.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipHMSModels.Models.EntityConfiguration
{
    public class ReservationStateConfiguration : EntityTypeConfiguration<ReservationState>
    {
        public ReservationStateConfiguration()
        {
            ToTable("ReservationState", "Basic");
            this.HasMany(m => m.ReservationState_Reservation_List)
                .WithRequired(r => r.Reservation_ReservationState);
        }
    }
}
