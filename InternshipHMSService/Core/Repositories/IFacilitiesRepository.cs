using InternshipHMSModels.Models;
using InternshipHMSModels.ViewModels;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipHMSService.Core.Repositories
{
    public interface IFacilitiesRepository : IRepository<Facilities>
    {
        DataSourceResult GetAllByDataSourceResult(DataSourceRequest request);
    }
}
