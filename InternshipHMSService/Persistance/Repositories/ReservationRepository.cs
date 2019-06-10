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
    public class ReservationRepository : Repository<Reservation>, IReservationRepository
    {
        public ReservationRepository(ApplicationDbContext db) : base(db)
        {
        }


        public bool CheckWithReservationsDates(Reservation reservation, string[] rooms)
        {
            List<Reservation> reservations = GetAll().ToList();
            foreach (Reservation item in reservations)
            {
                List<ReservationRooms> reservationsRooms = item.Reservation_ReservationRooms_List.ToList();
                foreach (ReservationRooms roomItem in reservationsRooms)
                {
                    foreach (string inputRoom in rooms)
                    {
                        if (
                            (roomItem.RoomID == Guid.Parse(inputRoom)) &&
                            (((reservation.FromDate <= item.FromDate) && (reservation.ToDate >= item.FromDate) && (reservation.FromDate <= item.ToDate && reservation.FromDate <= item.ToDate)) ||
                            ((reservation.FromDate >= item.FromDate) && (reservation.ToDate >= item.FromDate) && (reservation.FromDate <= item.ToDate && reservation.FromDate <= item.ToDate)) ||
                            ((reservation.FromDate >= item.FromDate) && (reservation.ToDate >= item.FromDate) && (reservation.FromDate <= item.ToDate && reservation.FromDate >= item.ToDate))))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }


        public bool AddWithRooms(Reservation reservation, string[] rooms)
        {
            

            foreach (string item in rooms)
            {
                _db.ReservationRooms_List.Add(new ReservationRooms()
                {
                    RoomID = Guid.Parse(item),
                    ReservationID = reservation.ID
                });
            }
            var result = Add(reservation);
            
            return result.Succeeded ? true : false;
        }
        public bool UpdateWithRooms(Reservation reservation, string[] rooms)
        {
            List<ReservationRooms> prevList = _db.ReservationRooms_List.Where(w => w.ReservationID == reservation.ID).ToList();
            foreach (ReservationRooms item in prevList)
            {
                _db.ReservationRooms_List.Remove(item);
            }
            _db.SaveChanges();
            foreach (string item in rooms)
            {
                _db.ReservationRooms_List.Add(new ReservationRooms()
                {
                    RoomID = Guid.Parse(item),
                    ReservationID = reservation.ID
                });
            }
            Reservation rsv = Find(reservation.ID);
            rsv.FromDate = reservation.FromDate;
            rsv.ToDate = reservation.ToDate;
            rsv.ReservationStateID = reservation.ReservationStateID;
            var result = Update(rsv);




            return result.Succeeded ? true : false;
        }
        public DataSourceResult GetAllByDataSourceResult(DataSourceRequest request)
        {
            return GetAll().OrderByDescending(o => o.Number).ToDataSourceResult(request, reservation => new
            {
                ID = reservation.ID,
                State = Translator.ReservationStateToPersian(reservation.Reservation_ReservationState.Title.ToString()),
                CustomerID = reservation.CustomerID,
                CustomerFullName = reservation.Reservation_Customer.FullName,
                Number = reservation.Number,
                FromDate = $"{PersianDateConvertor.ConvertToPersianDate(reservation.FromDate, true)}",
                ToDate = $"{PersianDateConvertor.ConvertToPersianDate(reservation.ToDate, true)}",
                DatePeriod = $"از {PersianDateConvertor.ConvertToPersianDate(reservation.FromDate)} تا {PersianDateConvertor.ConvertToPersianDate(reservation.ToDate)}"
            });
        }

        public DataSourceResult GetAllForCustomerByDataSourceResult(DataSourceRequest request, string id)
        {
            Guid customerId = Guid.Parse(id);
            return GetAll().Where(w => w.CustomerID == customerId).OrderByDescending(o => o.Number).ToDataSourceResult(request, reservation => new
            {
                ID = reservation.ID,
                State = reservation.Reservation_ReservationState.Title.ToString(),
                CustomerID = reservation.CustomerID,
                CustomerFullName = reservation.Reservation_Customer.FullName,
                Number = reservation.Number,
                FromDate = $"{PersianDateConvertor.ConvertToPersianDate(reservation.FromDate)}",
                ToDate = $"{PersianDateConvertor.ConvertToPersianDate(reservation.ToDate)}",
                DatePeriod = $"از {PersianDateConvertor.ConvertToPersianDate(reservation.FromDate)} تا {PersianDateConvertor.ConvertToPersianDate(reservation.ToDate)}"
            });
        }
    }
}
