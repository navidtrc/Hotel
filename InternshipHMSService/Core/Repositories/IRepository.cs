using InternshipHMSModels.Core.Entity.Base;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipHMSService.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : ObjectModel
    {
        TEntity Find(Guid Id);
        IEnumerable<TEntity> GetAll();
        IdentityResult Add(TEntity instance);
        IdentityResult Update(TEntity instance);
        IdentityResult Delete(Guid Id);
    }
}
