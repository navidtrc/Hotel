using InternshipHMSModels.Models;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipHMSService.Core.Repositories
{
    public interface IRoomRepository : IRepository<Room>
    {
        DataSourceResult GetAllByDataSourceResult(DataSourceRequest request);
        DataSourceResult GetRoomsForReservationInDate(DataSourceRequest request, DateTime fromDate, DateTime toDate,string reservationId);
        DataSourceResult GetFreeRooms(DataSourceRequest request, DateTime requestDate);

    }
}
