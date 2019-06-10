using InternshipHMSDataAccess;
using InternshipHMSModels.Models;
using InternshipHMSModels.ViewModels;
using InternshipHMSService.Core.Repositories;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipHMSService.Persistance.Repositories
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(ApplicationDbContext db) : base(db)
        {
        }

        public string GetPersonIdForEmail(string email)
        {
            return _db.Users.FirstOrDefault(f => f.Email == email).AplicationUser_Person.ID.ToString();

        }

        public string GetRoleNameForPersonId(string personId)
        {
            string roleId = _db.Users.FirstOrDefault(f => f.AplicationUser_Person.ID == Guid.Parse(personId)).Roles.FirstOrDefault().RoleId;
            return _db.Roles.FirstOrDefault(f => f.Id == roleId).Name.ToString();
        }

        public string GetRoleNameForUserId(string userId)
        {
            string roleId = _db.Users.FirstOrDefault(f => f.Id == userId).Roles.FirstOrDefault().RoleId;
            return _db.Roles.FirstOrDefault(f => f.Id == roleId).Name.ToString();
        }

        public string GetUserIdForEmail(string email)
        {
            return _db.Users.FirstOrDefault(f => f.Email == email).Id.ToString();
        }
        public void ResetPass(ResetPassViewModel password)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_db));
            UserStore<ApplicationUser> store = new UserStore<ApplicationUser>();
            Guid id = Guid.Parse(password.ID);
            ApplicationUser user = _db.Users.FirstOrDefault(w => w.AplicationUser_Person.ID == id);
            store.SetPasswordHashAsync(user, userManager.PasswordHasher.HashPassword(password.Password));
        }
    }
}
