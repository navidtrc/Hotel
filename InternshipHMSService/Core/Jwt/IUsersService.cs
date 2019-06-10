using InternshipHMSModels.Models;
using System.Collections.Generic;

namespace InternshipHMSService.Core.Jwt
{
    public interface IUsersService
    {
        string GetSerialNumber(string userId);
        IEnumerable<string> GetUserRoles(string userId);
        ApplicationUser FindUser(string username, string password);
        ApplicationUser FindUser(string userId);
        void UpdateUserLastActivityDate(string userId);
    }
}