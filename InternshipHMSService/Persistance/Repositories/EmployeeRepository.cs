using InternshipHMSDataAccess;
using InternshipHMSLibrary;
using InternshipHMSModels.Models;
using InternshipHMSModels.ViewModels;
using InternshipHMSService.Core.Repositories;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipHMSService.Persistance.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext db) : base(db)
        {
        }

        public bool AddWithUser(Employee employee, string currentUserId, string password)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_db));
            employee.Person_ApplicationUser.AplicationUser_Person = employee;
            ApplicationUser user = employee.Person_ApplicationUser;
            employee.Person_ApplicationUser = null;
            if (!Translator.RoleCheckChangingAccess(currentUserId, employee.RoleHelper))
                return false;
            Add(employee);
            var result = userManager.Create(user, password);
            if (result.Succeeded)
                userManager.AddToRole(user.Id, employee.RoleHelper);
            return true;
        }

        public DataSourceResult GetAllByDataSourceResult(DataSourceRequest request)
        {
            return GetAll().ToDataSourceResult(request, employee => new
            {
                ID = employee.ID,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                NationalCode = employee.NationalCode,
                EmployeeCode = employee.EmployeeCode,
                FullName = employee.FullName
            });
        }
        public override IdentityResult Add(Employee instance)
        {
            instance.Person_ApplicationUser = null;
            return base.Add(instance);
        }
        public override Employee Find(Guid Id)
        {
            Employee employee = _db.Employee_List.Find(Id);
            if (employee.Person_ApplicationUser != null)
                employee.RoleHelper = _db.Roles.Find(employee.Person_ApplicationUser.Roles.FirstOrDefault().RoleId.ToString()).Name;
            return employee;
        }

        public bool UpdateAndCreateUser(Employee employee, string currentUserId, string password)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_db));
            employee.Person_ApplicationUser.AplicationUser_Person = employee;
            ApplicationUser user = employee.Person_ApplicationUser;
            employee.Person_ApplicationUser = null;
            if (!Translator.RoleCheckChangingAccess(currentUserId, employee.RoleHelper))
                return false;
            Update(employee);
            var result = userManager.Create(user, password);
            if (result.Succeeded)
                userManager.AddToRole(user.Id, employee.RoleHelper);
            return true;
        }

        public bool UpdateWithUser(Employee employee,string currentUserId)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_db));
            employee.Person_ApplicationUser.AplicationUser_Person = employee;
            ApplicationUser user = employee.Person_ApplicationUser;
            user.Id = _db.Users.Where(w => w.AplicationUser_Person.ID == employee.ID).FirstOrDefault().Id;
            user.AplicationUser_Person.ID = employee.ID;
            string currentRoleID = _db.Users.FirstOrDefault(w => w.Id == user.Id).Roles.FirstOrDefault().RoleId.ToString();
            string currentRole = _db.Roles.FirstOrDefault(w => w.Id == currentRoleID).Name.ToString();
            if (!Translator.RoleCheckChangingAccess(currentUserId, employee.RoleHelper))
                return false;
            userManager.RemoveFromRoles(user.Id, currentRole);
            userManager.AddToRole(user.Id, employee.RoleHelper);
            employee.Person_ApplicationUser = null;
            Update(employee);
            _db.Set<ApplicationUser>().AddOrUpdate(user);
            return true;
        }

      

        public DataSourceResult GetContactInformation(DataSourceRequest request, Guid id)
        {

            return _db.ContactInformation_List.Where(w => w.PersonID == id)
                .ToDataSourceResult(request, contact => new
                {
                    ID = contact.ID,
                    PhoneNumber = contact.PhoneNumber,
                    Type = contact.ContactInformation_PhoneType == null ? "" : contact.ContactInformation_PhoneType.Title.ToString(),
                    Country = contact.Country,
                    City = contact.City,
                    Address = contact.Address

                });
        }
    }
}
