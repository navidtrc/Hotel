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
    public interface IEmployeeRepository : IRepository<Employee>
    {
        DataSourceResult GetAllByDataSourceResult(DataSourceRequest request);
        DataSourceResult GetContactInformation(DataSourceRequest request,Guid id);
        bool AddWithUser(Employee employee,string currentUserId, string password);
        bool UpdateAndCreateUser(Employee employee, string currentUserId, string password);
        bool UpdateWithUser(Employee employee, string currentUserId);
    }
}
