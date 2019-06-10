using InternshipHMSDataAccess;
using InternshipHMSLibrary;
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
    public class ReservationStateRepository : Repository<ReservationState>, IReservationStateRepository
    {
        public ReservationStateRepository(ApplicationDbContext db) : base(db)
        {
        }
        public DataSourceResult GetAllByDataSourceResult(DataSourceRequest request)
        {
            return GetAll().ToDataSourceResult(request, reservationState => new
            {
                ID = reservationState.ID,
                Title = reservationState.Title,
            });
        }
    }
}
