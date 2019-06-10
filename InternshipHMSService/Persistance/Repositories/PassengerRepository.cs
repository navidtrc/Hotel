using InternshipHMSDataAccess;
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
    public class PassengerRepository : Repository<Passenger>, IPassengerRepository
    {
        public PassengerRepository(ApplicationDbContext db) : base(db)
        {
        }
        public DataSourceResult GetAllByDataSourceResult(DataSourceRequest request)
        {
            return GetAll().ToDataSourceResult(request, passenger => new
            {
                ID = passenger.ID,
                CheckingID = passenger.CheckingID,
                CustomerID = passenger.CustomerID,
                CustomerFullName = passenger.Passenger_Customer.FullName
            });
        }

        public DataSourceResult GetAvailableCustomers(DataSourceRequest request)
        {


            return GetAll().Where(a => DateTime.Now.Date > a.Passenger_Checking.FromDate && DateTime.Now.Date < a.Passenger_Checking.ToDate).ToDataSourceResult(request, passenger => new
            {
                ID = passenger.ID,
                CheckingID = passenger.CheckingID,
                CustomerID = passenger.CustomerID,
                CustomerFullName = passenger.Passenger_Customer.FullName,
                NationalCode = passenger.Passenger_Customer.NationalCode,
                PassportNumber = passenger.Passenger_Customer.PassportNumber,
            });

        }

        public DataSourceResult GetRoomPassengersByDataSourceResult(DataSourceRequest request, string checkingId)
        {
            Guid guid = Guid.Parse(checkingId);
            return GetAll().Where(w => w.CheckingID == guid).ToDataSourceResult(request, passenger => new
            {
                ID = passenger.ID.ToString(),
                CustomerID = passenger.Passenger_Customer.ID.ToString(),
                FirstName = passenger.Passenger_Customer.FirstName,
                LastName = passenger.Passenger_Customer.LastName,
                NationalCode = passenger.Passenger_Customer.NationalCode,
                FullName = passenger.Passenger_Customer.FullName

            });
        }
    }
}
