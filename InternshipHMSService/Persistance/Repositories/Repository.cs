using InternshipHMSDataAccess;
using InternshipHMSModels.Core.Entity.Base;
using InternshipHMSService.Core.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipHMSService.Persistance.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : ObjectModel
    {
        protected readonly ApplicationDbContext _db;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
        }
        protected DbSet<TEntity> _entityCollection;
        protected DbSet<TEntity> EntityCollection
        {
            get
            {
                if (_entityCollection == null)
                    _entityCollection = _db.Set<TEntity>();
                return _entityCollection;
            }
        }


        public virtual IdentityResult Add(TEntity instance)
        {
            EntityCollection.Add(instance);
            return IdentityResult.Success;
        }

        public virtual IdentityResult Delete(Guid Id)
        {
            _db.Entry(EntityCollection.Find(Id)).State = EntityState.Deleted;
            return IdentityResult.Success;
        }

        public virtual TEntity Find(Guid Id)
        {
            return EntityCollection.Find(Id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return EntityCollection.ToList();
        }

        public virtual IdentityResult Update(TEntity instance)
        {
            _db.Entry(instance).State = EntityState.Modified;
            return IdentityResult.Success;
        }
    }
}
