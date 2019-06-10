using InternshipHMSModels.Models;
using Kendo.Mvc.UI;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipHMSService.Core.Repositories
{
    public interface ICheckingRepository : IRepository<Checking>
    {
        DataSourceResult GetAllByDataSourceResult(DataSourceRequest request);
        IdentityResult AddWithPassengers(Checking checking, string[] passengersId);
        IdentityResult EditWithPassengers(Checking checking, string[] passengersId);
        DataSourceResult GetAllByDataSourceResultInDate(DataSourceRequest request, DateTime requestDate);

    }
}
