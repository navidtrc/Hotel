using InternshipHMSDataAccess;
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
    public class FacilitiesRepository : Repository<Facilities>, IFacilitiesRepository
    {
        public FacilitiesRepository(ApplicationDbContext db) : base(db)
        {
        }
        


        public DataSourceResult GetAllByDataSourceResult(DataSourceRequest request)
        {
            return GetAll().ToDataSourceResult(request, bed => new
            {
                ID = bed.ID
                
            });
        }
    }
}
