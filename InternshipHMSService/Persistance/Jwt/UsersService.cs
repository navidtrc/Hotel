using InternshipHMSDataAccess;
using InternshipHMSModels.Models;
using InternshipHMSService.Core.Jwt;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InternshipHMSService.Persistance.Jwt
{
    public class UsersService : IUsersService
    {
        //TODO: replace it with `public IDbSet<User> Users {set;get;}`
        //private static readonly IList<User> _users = new List<User>
        //{
        //    // initial `seed`, just for the demo
        //    new User
        //    {
        //     UserId = 1,
        //     UserName = "Vahid",
        //     DisplayName = "وحيد",
        //     IsActive = true,
        //     LastLoggedIn = null,
        //     Password = new SecurityService().GetSha256Hash("1234"),
        //     Roles = new []{ "user", "Admin" },
        //     SerialNumber = Guid.NewGuid().ToString("N")
        //    }
        //};

        private readonly List<ApplicationUser> _users;
        public UsersService()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            _users = db.Users.ToList();
        }

        private readonly ISecurityService _securityService;
        public UsersService(ISecurityService securityService)
        {
            _securityService = securityService;
        }

        public ApplicationUser FindUser(string userId)
        {

            return _users.FirstOrDefault(x => x.Id == userId);
        }

        public ApplicationUser FindUser(string username, string password)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var allUser = db.Users.ToList();
            var selectedUser = allUser.FirstOrDefault(f => f.UserName == username);

            if (selectedUser != null)
            {
                PasswordHasher passwordHasher = new PasswordHasher();
                if (passwordHasher.VerifyHashedPassword(selectedUser.PasswordHash, password) != PasswordVerificationResult.Failed)

                {
                    return selectedUser;
                }
            }
            return null;
        }

        public string GetSerialNumber(string userId)
        {
            var user = FindUser(userId);
            return user.SerialNumber;
        }

        public IEnumerable<string> GetUserRoles(string userId)
        {
            var user = FindUser(userId);
            ApplicationDbContext db = new ApplicationDbContext();
            List<string> roles = new List<string>();
            foreach (var item in user.Roles)
            {
                roles.Add(db.Roles.FirstOrDefault(f => f.Id == item.RoleId).Name);
            }
            return roles;
        }

        public void UpdateUserLastActivityDate(string userId)
        {
            var user = FindUser(userId);
            user.LastLoggedIn = DateTime.UtcNow;
        }
    }
}