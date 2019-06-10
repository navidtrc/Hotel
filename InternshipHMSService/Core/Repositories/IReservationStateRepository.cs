using InternshipHMSModels.Models;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipHMSService.Core.Repositories
{
    public interface IReservationRepository : IRepository<Reservation>
    {
        DataSourceResult GetAllByDataSourceResult(DataSourceRequest request);
        DataSourceResult GetAllForCustomerByDataSourceResult(DataSourceRequest request,string id);
        bool AddWithRooms(Reservation reservation,string[] rooms);
        bool UpdateWithRooms(Reservation reservation, string[] rooms);
        bool CheckWithReservationsDates(Reservation reservation, string[] rooms);
    }
}
