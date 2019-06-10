using InternshipHMSDataAccess;
using InternshipHMSLibrary;
using InternshipHMSModels.Models;
using InternshipHMSService.Core.Repositories;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipHMSService.Persistance.Repositories
{
    public class ReservationRoomsRepository : Repository<ReservationRooms>, IReservationRoomsRepository
    {
        public ReservationRoomsRepository(ApplicationDbContext db) : base(db)
        {
        }


       


        public DataSourceResult GetAllByDataSourceResultInDate(DataSourceRequest request, DateTime requestDate)
        {
            if (GetAll().Where(w => w.ReservationRooms_Reservation.FromDate <= requestDate && w.ReservationRooms_Reservation.ToDate >= requestDate).Count() != 0)
            {
                return GetAll().Where(w => w.ReservationRooms_Reservation.FromDate <= requestDate && w.ReservationRooms_Reservation.ToDate >= requestDate).ToDataSourceResult(request, reservation => new
                {
                    ID = reservation.ID,
                    RequeestDate = PersianDateConvertor.ConvertToPersianDate(requestDate, true),
                    ReservationID = reservation.ReservationID,
                    RoomID = reservation.RoomID,
                    RoomNumber = reservation.ReservationRooms_Room.Number,
                    ReservationNumber = reservation.ReservationRooms_Reservation.Number,
                    ReservationType = Translator.ReservationStateToPersian(reservation.ReservationRooms_Reservation.Reservation_ReservationState.Title.ToString()),
                    DatePeriod = $"از تاریخ {PersianDateConvertor.ConvertToPersianDate(reservation.ReservationRooms_Reservation.FromDate, true)} تا {PersianDateConvertor.ConvertToPersianDate(reservation.ReservationRooms_Reservation.ToDate, true)}"
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

        public DataSourceResult GetRoomsForReservationByDataSourceResult(DataSourceRequest request, string reservationId)
        {
            Guid reserveId = Guid.Parse(reservationId);
            return GetAll().Where(w => w.ReservationRooms_Reservation.ID == reserveId).ToDataSourceResult(request, reservation => new
            {
                ID = reservation.ReservationRooms_Room.ID,
                Number = reservation.ReservationRooms_Room.Number,
                Location = reservation.ReservationRooms_Room.Location,
                Type = reservation.ReservationRooms_Room.Room_RoomType.Type.ToString(),
            });
        }
    }
}
