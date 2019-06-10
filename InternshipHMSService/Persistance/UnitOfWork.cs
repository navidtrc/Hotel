using InternshipHMSDataAccess;
using InternshipHMSService.Core;
using InternshipHMSService.Core.Repositories;
using InternshipHMSService.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipHMSService.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Customer_List = new CustomerRepository(_db);
            Employee_List = new EmployeeRepository(_db);
            HotelData_List = new HotelDataRepository(_db);
            Room_List = new RoomRepository(_db);
            RoomType_List = new RoomTypeRepository(_db);
            ContactInformation_List = new ContactInformationRepository(_db);
            Person_List = new PersonRepository(_db);
            Reservation_List = new ReservationRepository(_db);
            ReservationState_List = new ReservationStateRepository(_db);
            ReservationRooms_List = new ReservationRoomsRepository(_db);
            Checking_List = new CheckingRepository(_db);
            Passenger_List = new PassengerRepository(_db);
            RoomPrice_List = new RoomPriceRepository(_db);
            Facilities_List = new FacilitiesRepository(_db);
        }
        public ICustomerRepository Customer_List { get; private set; }
        public IEmployeeRepository Employee_List { get; private set; }
        public IHotelDataRepository HotelData_List { get; private set; }
        public IRoomRepository Room_List { get; private set; }
        public IRoomTypeRepository RoomType_List { get; private set; }
        public IContactInformationRepository ContactInformation_List { get; private set; }
        public IPersonRepository Person_List { get; private set; }
        public IReservationRepository Reservation_List { get; private set; }
        public IReservationStateRepository ReservationState_List { get; private set; }
        public IReservationRoomsRepository ReservationRooms_List { get; private set; }
        public ICheckingRepository Checking_List { get; private set; }
        public IPassengerRepository Passenger_List { get; private set; }
        public IRoomPriceRepository RoomPrice_List { get; private set; }
        public IFacilitiesRepository Facilities_List { get; private set; }

        public int Complete()
        {
            return _db.SaveChanges();
        }
        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
