using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using QM.DataAccess.Data;
using QM.DataAccess.Repo.IRepo;
using QM.DataAccess.Repo;
using QM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace QM.DataAccess.Repo
{
    public class Repo<T> : IRepo<T> where T : class
    {


        private readonly ApplicationDbContext _db;
                    
        private DbSet<T> dbSet;
        public Repo(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }
                     
        public async Task<List<T>> FindAllAsync(Expression<Func<T, bool>>? filter = null, string? orderBy = null, Pagger? paggerBy = null, List<String>? include = null)
        {
            // 1. Start with the DbSet
            IQueryable<T> query = dbSet;

            // 2. Apply Eager Loading (Include)
            if (include != null)
            {
                foreach (var inc in include)
                {
                    query = query.Include(inc);
                }
            }

            // 3. Apply Filtering
            if (filter != null)
            {
                query = query.Where(filter);
            }

            // 4. Apply Ordering (String-based)
            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                query = query.OrderBy(orderBy);
            }

            // 5. Apply Pagination
            if (paggerBy != null)
            {
                // Assuming your Pagger class has PageNumber (1-based) and PageSize
                int skip = (paggerBy.PageIndex - 1) * paggerBy.PageSize;
                query = query.Skip(skip).Take(paggerBy.PageSize);
            }

            // 6. Execute
            return await query.ToListAsync();


        }

        public async Task<T> GetByIdAsync(int id)
        {
            // Use FindAsync with a single key value
            var entity = await dbSet.FindAsync(id);
            // If entity is null, handle accordingly (optional)
            return entity!;
        }


        public async Task<T> AddUpdateAsync(T entity)
        {
            dbSet.Update(entity);

            return await Task.FromResult(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            // Remove is also synchronous in EF Core (just marks it as "Deleted" in memory).
            dbSet.Remove(entity);

            // We return a completed task to satisfy the async interface signature.
            await Task.CompletedTask;
        }


        public async Task AddUpdateAsync(IEnumerable<T> entities)
        {
            // The "UpdateRange" method in EF Core is very smart.
            // 1. It looks at every item in the list.
            // 2. If an item has ID=0, it marks it to be ADDED.
            // 3. If an item has ID > 0, it marks it to be UPDATED.
            dbSet.UpdateRange(entities);

            // Since UpdateRange is synchronous (it just modifies the tracker in RAM),
            // we return a completed task to satisfy the 'async' signature.
            await Task.CompletedTask;
        }


    }
}

