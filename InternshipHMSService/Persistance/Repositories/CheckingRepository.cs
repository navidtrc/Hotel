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
    public class CheckingRepository : Repository<Checking>, ICheckingRepository
    {
        public CheckingRepository(ApplicationDbContext db) : base(db)
        {
        }

        public IdentityResult AddWithPassengers(Checking checking, string[] passengersId)
        {
            Add(checking);

            foreach (string item in passengersId)
            {
                _db.Passenger_List.Add(new Passenger()
                {
                    CheckingID = checking.ID,
                    CustomerID = Guid.Parse(item)
                });
            }
            return IdentityResult.Success;
        }

        public IdentityResult EditWithPassengers(Checking checking, string[] passengersId)
        {
            Update(checking);
            List<Passenger> oldPassenger = _db.Passenger_List.Where(w => w.CheckingID == checking.ID).ToList();
            foreach (Passenger item in oldPassenger)
            {
                _db.Passenger_List.Remove(item);
            }
            foreach (string item in passengersId)
            {
                _db.Passenger_List.Add(new Passenger()
                {
                    CheckingID = checking.ID,
                    CustomerID = Guid.Parse(item)
                });
            }
            return IdentityResult.Success;
        }

        public DataSourceResult GetAllByDataSourceResult(DataSourceRequest request)
        {

            //Guid? reservationId = null;

            return GetAll().OrderByDescending(d => d.CreateDate).ToDataSourceResult(request, checking => new
            {
                ID = checking.ID,
                RoomID = checking.Checking_Room.ID,
                RoomNumber = checking.Checking_Room.Number,
                FromDate = PersianDateConvertor.ConvertToPersianDate(checking.FromDate, true),
                ToDate = PersianDateConvertor.ConvertToPersianDate(checking.ToDate, true),
                ReservationID = checking.ReservationID,
                ReservationNumber = checking.Checking_Reservation == null ? "" : checking.Checking_Reservation.Number.ToString(),
            });
        }

        public DataSourceResult GetAllByDataSourceResultInDate(DataSourceRequest request, DateTime requestDate)
        {
            if (GetAll().Where(w => w.FromDate <= requestDate && w.ToDate >= requestDate).Count() != 0)
            {
                return GetAll().Where(w => w.FromDate <= requestDate && w.ToDate >= requestDate).ToDataSourceResult(request, checking => new
                {
                    ID = checking.ID,
                    RequeestDate = PersianDateConvertor.ConvertToPersianDate(requestDate, true),
                    ReservationID = checking.ReservationID,
                    RoomID = checking.RoomID,
                    RoomNumber = checking.Checking_Room.Number,
                    ReservationNumber = checking.Checking_Reservation == null ? "" : checking.Checking_Reservation.Number.ToString(),
                    DatePeriod = $"از تاریخ {PersianDateConvertor.ConvertToPersianDate(checking.FromDate, true)} تا {PersianDateConvertor.ConvertToPersianDate(checking.ToDate, true)}"
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


    }
}
