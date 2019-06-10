using InternshipHMSModels.Models;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipHMSService.Core.Repositories
{
    public interface IHotelDataRepository : IRepository<HotelData>
    {
        DataSourceResult GetAllByDataSourceResult(DataSourceRequest request);

    }
}
