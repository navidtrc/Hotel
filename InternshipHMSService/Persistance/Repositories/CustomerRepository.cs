using InternshipHMSDataAccess;
using InternshipHMSModels.Models;
using InternshipHMSService.Core.Repositories;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipHMSService.Persistance.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext db) : base(db)
        {

        }

        public IdentityResult UpdateAndCreateUser(Customer customer, string password)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_db));
            customer.Person_ApplicationUser.AplicationUser_Person = customer;
            ApplicationUser user = customer.Person_ApplicationUser;
            customer.Person_ApplicationUser = null;
            Update(customer);
            var result = userManager.Create(user, password);
            if (result.Succeeded)
                userManager.AddToRole(user.Id, "95a75089-f400-4e40-ab94-def3626f5374");
            return IdentityResult.Success;
            //if (dsf)
            //{
            //    throw new ArgumentException("sdsd");
            //}
        }

        public IdentityResult UpdateWithUser(Customer customer)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_db));
            customer.Person_ApplicationUser.AplicationUser_Person = customer;
            ApplicationUser user = customer.Person_ApplicationUser;
            user.Id = _db.Users.Where(w => w.AplicationUser_Person.ID == customer.ID).FirstOrDefault().Id;
            user.AplicationUser_Person.ID = customer.ID;

            customer.Person_ApplicationUser = null;
            Update(customer);
            _db.Set<ApplicationUser>().AddOrUpdate(user);
            return IdentityResult.Success;
        }



        public IdentityResult AddWithUser(Customer customer, string password)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_db));
            customer.Person_ApplicationUser.AplicationUser_Person = customer;
            ApplicationUser user = customer.Person_ApplicationUser;
            customer.Person_ApplicationUser = null;
            Add(customer);
            var result = userManager.Create(user, password);
            if (result.Succeeded)
                userManager.AddToRole(user.Id, "95a75089-f400-4e40-ab94-def3626f5374");
            return IdentityResult.Success;
        }
        public override IdentityResult Add(Customer instance)
        {
            instance.Person_ApplicationUser = null;
            return base.Add(instance);
        }

        public DataSourceResult GetAllByDataSourceResult(DataSourceRequest request)
        {
            return GetAll().OrderByDescending(d => d.CreateDate).ToDataSourceResult(request, customer => new
            {
                ID = customer.ID,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                NationalCode = customer.NationalCode,
                CustomerCode = customer.CustomerCode,
                PassportNumber = customer.PassportNumber,
                FullName = customer.FullName
            });
        }

        public DataSourceResult GetChildByDataSourceResult(DataSourceRequest request, string id)
        {
            return GetAll().OrderByDescending(d => d.CreateDate).Where(w => w.ParentID == Guid.Parse(id)).ToDataSourceResult(request, customer => new
            {
                ID = customer.ID,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                NationalCode = customer.NationalCode,
                CustomerCode = customer.CustomerCode,
                PassportNumber = customer.PassportNumber,
                FullName = customer.FullName
            });
        }

        public DataSourceResult GetAllExceptIdByDataSourceResult(DataSourceRequest request, string parentId)
        {
            List<Customer> customers = GetAll().ToList();
            Customer parentCustomer = customers.FirstOrDefault(f => f.ID == Guid.Parse(parentId));
            customers.Remove(parentCustomer);
            return customers.ToDataSourceResult(request, customer => new
            {
                ID = customer.ID,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                NationalCode = customer.NationalCode,
                CustomerCode = customer.CustomerCode,
                PassportNumber = customer.PassportNumber,
                FullName = customer.FullName
            });
        }

        public IdentityResult ConvertCustomersToFellow(string parentId, string[] fellows)
        { 
            //Guid parentIdGuid = Guid.Parse(parentId);
            //foreach (Customer CustomerItem in GetAll().Where(w => w.ParentID == parentIdGuid))
            //{
            //    foreach (var ReserveItem in CustomerItem.Customer_Reservation_List)
            //    {
            //        if (ReserveItem.FromDate.Date < DateTime.Now.Date)
            //            CustomerItem.ParentID = null;
            //    }
            //}
            foreach (string item in fellows)
            {
                Customer instance = Find(Guid.Parse(item));
                instance.ParentID = Guid.Parse(parentId);
                Update(instance);
            }
            return IdentityResult.Success;
        }
        public IdentityResult UpdateFellows(string parentId, string[] fellows)
        {
            Guid parentGuid = Guid.Parse(parentId);
            List<Customer> prevFellows = _db.Customer_List.Where(w => w.ParentID == parentGuid).ToList();
            foreach (Customer item in prevFellows)
            {
                item.ParentID = null;
            }
            _db.SaveChanges();
            foreach (string item in fellows)
            {
                Customer instance = Find(Guid.Parse(item));
                instance.ParentID = Guid.Parse(parentId);
                Update(instance);
            }
            return IdentityResult.Success;
        }
        public override IdentityResult Delete(Guid Id)
        {
            var user = _db.Users.FirstOrDefault(w => w.AplicationUser_Person.ID == Id);
            if (user != null)
                _db.Users.Remove(user);
            return base.Delete(Id);
        }

    }
}
