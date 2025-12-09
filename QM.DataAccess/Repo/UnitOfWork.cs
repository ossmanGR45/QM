using QM.DataAccess.Data;
using QM.DataAccess.Repo.IRepo;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QM.DataAccess.Repo
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        // Dictionary to store repositories we have already created (Caching)
        private Dictionary<Type, object> _repositories;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            _repositories = new Dictionary<Type, object>();
        }

        // The "Best Practice" way to get a Generic Repo without hardcoding properties
        public IRepo<T> Repository<T>() where T : class
        {
            // If we already created this repo, return it
            if (_repositories.ContainsKey(typeof(T)))
            {
                return _repositories[typeof(T)] as IRepo<T>;
            }

            // Otherwise, create a new one and save it
            var repo = new Repo<T>(_db);
            _repositories.Add(typeof(T), repo);
            return repo;
        }

        public async Task<int> SaveAsync()
        {
            return await _db.SaveChangesAsync();
        }
    }
}