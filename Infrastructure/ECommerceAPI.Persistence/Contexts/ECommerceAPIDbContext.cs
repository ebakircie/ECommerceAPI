using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Contexts
{
    public class ECommerceAPIDbContext : DbContext
    {
        public ECommerceAPIDbContext(DbContextOptions options) : base(options)
        {}
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders{ get; set; }
        public DbSet<Customer> Customers{ get; set; }

        // NOTE: Interceptors;
        //are intercepting between the process of beginning and ending. Manipulates the neccessary things before finish the process. 
        //
        //                      BEGINNING-----------------------------------INTERCEPTION-----------------------------------
        //
        // For example normally we would add an entity to database but at the below, we intercept it before it finish it's task. changing specific properties and saving to database. 
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //ChangeTracker: is a property that helps us to find changes made on or newly added data over entities. Allows us to capture and obtain the Tracked data in Update operations.

            var datas = ChangeTracker
                .Entries<BaseEntity>();
            foreach (var item in datas)
            {
                 _ = item.State switch
                {
                    EntityState.Added => item.Entity.CreatedDate = DateTime.UtcNow,
                    EntityState.Modified => item.Entity.UpdatedDate = DateTime.UtcNow,
                    _ => DateTime.UtcNow  // bunu yazmasaydık, delete işleminde, switch yapısında added yada modified değilse, sonuncu şart'a giriyor. delete edilmiş bir objeyi etmeye çalıştığı için patladı.
                };
            }


            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
