using InternshipHMSModels.Models;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipHMSService.Core.Repositories
{
    public interface IRoomPriceRepository : IRepository<RoomPrice>
    {
        DataSourceResult GetForRoomWithDateByDataSourceResult(DataSourceRequest request,Guid roomId,DateTime requestDate);
        long RoomFinalPriceByDataSourceResult(DataSourceRequest request, Guid roomId, Guid reservationId);


    }
}
