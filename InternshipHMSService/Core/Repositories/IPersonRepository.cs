using InternshipHMSModels.Models;
using InternshipHMSModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipHMSService.Core.Repositories
{
    public interface IPersonRepository : IRepository<Person>
    {
        string GetRoleNameForUserId(string userId);
        string GetRoleNameForPersonId(string personId);
        string GetUserIdForEmail(string email);
        string GetPersonIdForEmail(string email);
        void ResetPass(ResetPassViewModel password);
    }
}
