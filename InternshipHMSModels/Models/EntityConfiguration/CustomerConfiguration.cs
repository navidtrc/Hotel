using InternshipHMSModels.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipHMSModels.Models.EntityConfiguration
{
    public class CustomerConfiguration : EntityTypeConfiguration<Customer>
    {
        public CustomerConfiguration()
        {
            this.ToTable("Customer", "Basic");
            this.HasMany(m => m.Customer_Reservation_List)
                .WithRequired(r => r.Reservation_Customer);
            this.HasMany(m => m.Customer_Passenger_List)
                .WithRequired(r => r.Passenger_Customer);

        }
    }
}
