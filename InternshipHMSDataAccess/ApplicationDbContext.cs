using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using InternshipHMSModels;
using InternshipHMSModels.Models;
using InternshipHMSModels.Models.EntityConfiguration;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace InternshipHMSDataAccess
{

    public partial class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<ContactInformation> ContactInformation_List { get; set; }
        public DbSet<Person> Person_List { get; set; }
        public DbSet<PhoneType> PhoneType_List { get; set; }
        public DbSet<Customer> Customer_List { get; set; }
        public DbSet<Employee> Employee_List { get; set; }
        public DbSet<HotelData> HotelData_List { get; set; }
        public DbSet<Room> Room_List { get; set; }
        public DbSet<RoomType> RoomType_List { get; set; }
        public DbSet<ReservationRooms> ReservationRooms_List { get; set; }
        public DbSet<Reservation> Reservation_List { get; set; }
        public DbSet<ReservationState> ReservationState_List { get; set; }
        public DbSet<Checking> Checking_List { get; set; }
        public DbSet<Passenger> Passenger_List { get; set; }
        public DbSet<Facilities> Facilities_List { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>().ToTable("User", "Security");
            modelBuilder.Entity<IdentityRole>().ToTable("Role", "Security");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRole", "Security");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaim", "Security");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogin", "Security");
            modelBuilder.Configurations.Add(new ContactInformationConfiguration());
            modelBuilder.Configurations.Add(new CustomerConfiguration());
            modelBuilder.Configurations.Add(new EmployeeConfiguration());
            modelBuilder.Configurations.Add(new FacilitiesConfiguration());
            modelBuilder.Configurations.Add(new HotelDataConfiguration());
            modelBuilder.Configurations.Add(new PersonConfiguration());
            modelBuilder.Configurations.Add(new PhoneTypeConfiguration());
            modelBuilder.Configurations.Add(new RoomConfiguration());
            modelBuilder.Configurations.Add(new RoomImagesConfiguration());
            modelBuilder.Configurations.Add(new RoomTypeConfiguration());

            modelBuilder.Configurations.Add(new CheckingConfiguration());
            modelBuilder.Configurations.Add(new PassengerConfiguration());
            modelBuilder.Configurations.Add(new ReservationConfiguration());
            modelBuilder.Configurations.Add(new ReservationRoomsConfiguration());
            modelBuilder.Configurations.Add(new ReservationStateConfiguration());
            modelBuilder.Configurations.Add(new RoomPriceConfiguration());

        }

        
    }
}