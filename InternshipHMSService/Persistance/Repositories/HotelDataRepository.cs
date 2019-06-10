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
    public class HotelDataRepository : Repository<HotelData>, IHotelDataRepository
    {
        public HotelDataRepository(ApplicationDbContext db) : base(db)
        {
        }
        public DataSourceResult GetAllByDataSourceResult(DataSourceRequest request)
        {
            return GetAll().ToDataSourceResult(request, hotel => new
            {
                ID = hotel.ID,
                Name = hotel.Name,
                Rate = hotel.Rate
            });
        }
    }
}
