using ECommerceAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Repositories
{
    public interface IReadRepository<T> : IRepository<T> where T: BaseEntity
    {
        IQueryable<T> GetAll(bool tracking = true);
        IQueryable<T> GetWhere(Expression<Func<T,bool>> method, bool tracking = true);
        Task<T> GetSingleAsync(Expression<Func<T,bool>> method, bool tracking = true);
        Task<T> GetByIdAsync(string id, bool tracking = true);        
    }
}
//NOTE: Tracking and NoTracking
//EntityFramework Core has Tracking mechanism which is responsible for tracking the changes made upon entity. When we retrive entities (doesn't matter how many, whole data or just one) EF store them in cache and check whatever changes made (add update delete ) until SaveChanges called. So when your query is meant for read operations we dont have to track entities to save Memory usage.
