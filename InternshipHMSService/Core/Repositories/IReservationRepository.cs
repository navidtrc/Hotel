using InternshipHMSModels.Models;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipHMSService.Core.Repositories
{
    public interface IReservationStateRepository : IRepository<ReservationState>
    {
        DataSourceResult GetAllByDataSourceResult(DataSourceRequest request);
    }
}
