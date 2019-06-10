using InternshipHMSService.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipHMSService.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IContactInformationRepository ContactInformation_List { get; }
        ICustomerRepository Customer_List { get; }
        IEmployeeRepository Employee_List { get; }
        IHotelDataRepository HotelData_List { get; }
        IRoomRepository Room_List { get; }
        IRoomTypeRepository RoomType_List { get; }
        IPersonRepository Person_List { get; }
        IReservationRepository Reservation_List { get; }
        IReservationStateRepository ReservationState_List { get; }
        IReservationRoomsRepository ReservationRooms_List { get; }
        ICheckingRepository Checking_List { get; }
        IPassengerRepository Passenger_List { get; }
        IRoomPriceRepository RoomPrice_List { get; }
        IFacilitiesRepository Facilities_List { get; }


    int Complete();
    }
}
