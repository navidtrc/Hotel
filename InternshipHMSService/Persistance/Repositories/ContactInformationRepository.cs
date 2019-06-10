using InternshipHMSDataAccess;
using InternshipHMSModels.Models;
using InternshipHMSService.Core.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipHMSService.Persistance.Repositories
{
    public class ContactInformationRepository : Repository<ContactInformation>, IContactInformationRepository
    {
        public ContactInformationRepository(ApplicationDbContext db) : base(db)
        {
        }
        public override IdentityResult Add(ContactInformation instance)
        {
            if (_db.PhoneType_List.Where(f => f.Title == instance.Type).Count() > 0)
                instance.PhoneTypeID = _db.PhoneType_List.FirstOrDefault(f => f.Title == instance.Type).ID;
            else
            {
                _db.PhoneType_List.Add(new PhoneType() { Title = instance.Type });
                _db.SaveChanges();
                instance.PhoneTypeID = _db.PhoneType_List.FirstOrDefault(f => f.Title == instance.Type).ID;
            }
            return base.Add(instance);
        }
        public override IdentityResult Update(ContactInformation instance)
        {
            if (_db.PhoneType_List.Where(f => f.Title == instance.Type).Count() > 0)
                instance.PhoneTypeID = _db.PhoneType_List.FirstOrDefault(f => f.Title == instance.Type).ID;
            else
            {
                _db.PhoneType_List.Add(new PhoneType() { Title = instance.Type });
                _db.SaveChanges();
                instance.PhoneTypeID = _db.PhoneType_List.FirstOrDefault(f => f.Title == instance.Type).ID;
            }
            return base.Update(instance);
        }
    }
}
