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
    public interface ICustomerRepository : IRepository<Customer>
    {
        IdentityResult AddWithUser(Customer customer, string password);
        DataSourceResult GetAllByDataSourceResult(DataSourceRequest request);
        DataSourceResult GetAllExceptIdByDataSourceResult(DataSourceRequest request,string parentId);
        DataSourceResult GetChildByDataSourceResult(DataSourceRequest request, string id);
        IdentityResult UpdateWithUser(Customer customer);
        IdentityResult UpdateAndCreateUser(Customer customer, string password);
        IdentityResult ConvertCustomersToFellow(string parentId, string[] fellows);
        IdentityResult UpdateFellows(string parentId, string[] fellows);
    }
}
