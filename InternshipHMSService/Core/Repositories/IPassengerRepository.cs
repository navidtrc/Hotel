using InternshipHMSModels.Models;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipHMSService.Core.Repositories
{
    public interface IPassengerRepository : IRepository<Passenger>
    {
        DataSourceResult GetAllByDataSourceResult(DataSourceRequest request);
        DataSourceResult GetRoomPassengersByDataSourceResult(DataSourceRequest request,string checkingId);
        DataSourceResult GetAvailableCustomers(DataSourceRequest request);

    }
}
