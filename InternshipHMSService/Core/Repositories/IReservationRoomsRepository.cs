using InternshipHMSModels.Models;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipHMSService.Core.Repositories
{
    public interface IReservationRoomsRepository : IRepository<ReservationRooms>
    {
        DataSourceResult GetAllByDataSourceResultInDate(DataSourceRequest request,DateTime requestDate);
        DataSourceResult GetRoomsForReservationByDataSourceResult(DataSourceRequest request, string reservationId);

       

    }
}
