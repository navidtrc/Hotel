using InternshipHMSDataAccess;
using InternshipHMSLibrary;
using InternshipHMSModels.Models;
using InternshipHMSService.Core.Repositories;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipHMSService.Persistance.Repositories
{
    public class RoomRepository : Repository<Room>, IRoomRepository
    {
        public RoomRepository(ApplicationDbContext db) : base(db)
        {
        }


        public DataSourceResult GetAllByDataSourceResult(DataSourceRequest request)
        {
            return GetAll().OrderBy(r => r.Number).ToDataSourceResult(request, room => new
            {
                ID = room.ID.ToString(),
                Number = room.Number.ToString(),
                Location = room.Location.ToString(),
                Type = room.Room_RoomType.Type.ToString(),
                HotelInfo = room.Room_HotelData.Name.ToString()

            });
        }

        public DataSourceResult GetFreeRooms(DataSourceRequest request, DateTime requestDate)
        {

            List<Room> freeRooms = new List<Room>();

            foreach (Room roomItem in GetAll())
            {
                bool AddOrNot = true;
                if (roomItem.Room_Checking_List != null)
                {
                    foreach (Checking checkingItem in roomItem.Room_Checking_List)
                    {
                        if ((requestDate > checkingItem.FromDate && requestDate > checkingItem.ToDate) || (requestDate < checkingItem.FromDate && requestDate < checkingItem.ToDate))
                            continue;
                        AddOrNot = false;
                    }
                }
                if (roomItem.Room_ReservationRooms_List != null)
                {
                    foreach (ReservationRooms reservationItem in roomItem.Room_ReservationRooms_List)
                    {
                        if ((requestDate > reservationItem.ReservationRooms_Reservation.FromDate && requestDate > reservationItem.ReservationRooms_Reservation.ToDate) || (requestDate < reservationItem.ReservationRooms_Reservation.FromDate && requestDate < reservationItem.ReservationRooms_Reservation.ToDate))
                            continue;
                        AddOrNot = false;
                    }
                }
                if (AddOrNot)
                    freeRooms.Add(roomItem);
            }


            if (freeRooms.Count != 0)
            {
                return freeRooms.ToDataSourceResult(request, rooms => new
                {
                    ID = rooms.ID,
                    RequeestDate = PersianDateConvertor.ConvertToPersianDate(requestDate, true),
                    RoomNumber = rooms.Number
                });
            }
            else
            {

                List<string> RequeestDate = new List<string>();
                RequeestDate.Add(PersianDateConvertor.ConvertToPersianDate(requestDate, true));
                DataSourceResult result = new DataSourceResult();
                result.Data = RequeestDate;
                return result;

            }
        }

        public DataSourceResult GetRoomsForReservationInDate(DataSourceRequest request, DateTime FromDate, DateTime ToDate, string ReservationId)
        {

            List<Room> FreeRooms = new List<Room>();
            bool AddOrNot = false;
            foreach (Room RoomItem in GetAll().ToList())
            {
                AddOrNot = true;
                foreach (ReservationRooms ReservationItem in RoomItem.Room_ReservationRooms_List.ToList())
                {



                    if ((Translator.ReservationStateToPersian(ReservationItem.ReservationRooms_Reservation.Reservation_ReservationState.Title.ToString()) != "لغو") &&
                        (((FromDate <= ReservationItem.ReservationRooms_Reservation.FromDate) && (ToDate >= ReservationItem.ReservationRooms_Reservation.FromDate) && (FromDate <= ReservationItem.ReservationRooms_Reservation.ToDate && FromDate <= ReservationItem.ReservationRooms_Reservation.ToDate)) ||
                        ((FromDate >= ReservationItem.ReservationRooms_Reservation.FromDate) && (ToDate >= ReservationItem.ReservationRooms_Reservation.FromDate) && (FromDate <= ReservationItem.ReservationRooms_Reservation.ToDate && FromDate <= ReservationItem.ReservationRooms_Reservation.ToDate)) ||
                        ((FromDate <= ReservationItem.ReservationRooms_Reservation.FromDate) && (ToDate >= ReservationItem.ReservationRooms_Reservation.FromDate) && (FromDate <= ReservationItem.ReservationRooms_Reservation.ToDate && FromDate >= ReservationItem.ReservationRooms_Reservation.ToDate)) ||
                        ((FromDate >= ReservationItem.ReservationRooms_Reservation.FromDate) && (ToDate >= ReservationItem.ReservationRooms_Reservation.FromDate) && (FromDate <= ReservationItem.ReservationRooms_Reservation.ToDate && FromDate >= ReservationItem.ReservationRooms_Reservation.ToDate))))
                        AddOrNot = false;

                    if (!string.IsNullOrEmpty(ReservationId))
                    {

                        if (ReservationItem.ReservationID == Guid.Parse(ReservationId))
                        {
                            AddOrNot = true;

                            foreach (ReservationRooms checkWithAnotherReservation in RoomItem.Room_ReservationRooms_List.ToList())
                            {
                                if (checkWithAnotherReservation.ID == ReservationItem.ID)
                                    continue;

                                if ((Translator.ReservationStateToPersian(ReservationItem.ReservationRooms_Reservation.Reservation_ReservationState.Title.ToString()) != "لغو") &&
                                   (((FromDate <= checkWithAnotherReservation.ReservationRooms_Reservation.FromDate) && (ToDate >= checkWithAnotherReservation.ReservationRooms_Reservation.FromDate) && (FromDate <= checkWithAnotherReservation.ReservationRooms_Reservation.ToDate && FromDate <= checkWithAnotherReservation.ReservationRooms_Reservation.ToDate)) ||
                                   ((FromDate >= checkWithAnotherReservation.ReservationRooms_Reservation.FromDate) && (ToDate >= checkWithAnotherReservation.ReservationRooms_Reservation.FromDate) && (FromDate <= checkWithAnotherReservation.ReservationRooms_Reservation.ToDate && FromDate <= checkWithAnotherReservation.ReservationRooms_Reservation.ToDate)) ||
                                   ((FromDate <= checkWithAnotherReservation.ReservationRooms_Reservation.FromDate) && (ToDate >= checkWithAnotherReservation.ReservationRooms_Reservation.FromDate) && (FromDate <= checkWithAnotherReservation.ReservationRooms_Reservation.ToDate && FromDate >= checkWithAnotherReservation.ReservationRooms_Reservation.ToDate)) ||
                                   ((FromDate >= checkWithAnotherReservation.ReservationRooms_Reservation.FromDate) && (ToDate >= checkWithAnotherReservation.ReservationRooms_Reservation.FromDate) && (FromDate <= checkWithAnotherReservation.ReservationRooms_Reservation.ToDate && FromDate >= checkWithAnotherReservation.ReservationRooms_Reservation.ToDate))))
                                    AddOrNot = false;
                            }
                        }

                    }

                }
                foreach (Checking CheckingItem in RoomItem.Room_Checking_List.ToList())
                {
                    if (((FromDate <= CheckingItem.FromDate) && (ToDate >= CheckingItem.FromDate) && (FromDate <= CheckingItem.ToDate && FromDate <= CheckingItem.ToDate)) ||
                        ((FromDate >= CheckingItem.FromDate) && (ToDate >= CheckingItem.FromDate) && (FromDate <= CheckingItem.ToDate && FromDate <= CheckingItem.ToDate)) ||
                        ((FromDate <= CheckingItem.FromDate) && (ToDate >= CheckingItem.FromDate) && (FromDate <= CheckingItem.ToDate && FromDate >= CheckingItem.ToDate)) ||
                        ((FromDate >= CheckingItem.FromDate) && (ToDate >= CheckingItem.FromDate) && (FromDate <= CheckingItem.ToDate && FromDate >= CheckingItem.ToDate)))
                        AddOrNot = false;
                }
                if (AddOrNot)
                    FreeRooms.Add(RoomItem);
            }
            return FreeRooms.ToDataSourceResult(request, room => new
            {
                ID = room.ID.ToString(),
                Number = room.Number.ToString(),
                Hotel = room.Room_HotelData.Name,
                Location = room.Location.ToString(),
                Type = room.Room_RoomType.Type.ToString(),
                HotelInfo = room.Room_HotelData.Name.ToString(),
                Price = room.Room_RoomPrice_List.Count == 0 ? "" : room.Room_RoomPrice_List.OrderByDescending(d => d.CreateDate.Date).FirstOrDefault(w => w.Date.Date == FromDate.Date).Price.ToString()

            });


        }

    }
}
