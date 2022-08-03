using ECommerceAPI.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Repositories
{
    public interface IRepository<T> where T : BaseEntity // T must be reference type which means class. constraint is needed!
    {
        DbSet<T> Table { get; }
    }
}

//Eventhough we use Generic Repository Design Pattern, it is against the SOLID principle 'S'.
// Single-Responsibility Principle;
// Querries and Data manipulations (add update delete) functions are in same class. Thats why its against Single-Responsibility Principle. Those functions has different functionality thats why we have to divide them instead of using them in IRepository. As a principal perspect we should use like IReadRepository and IWriteRepository. At the same time wefollow SOLID 'I' Interface Segregation Principle while seperating interfaces instead of centralising.
