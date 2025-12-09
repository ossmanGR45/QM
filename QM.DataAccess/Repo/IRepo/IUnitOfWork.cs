using QM.DataAccess.Data;
using System;
using System.Threading.Tasks;

namespace QM.DataAccess.Repo.IRepo
{
    public interface IUnitOfWork 
    {
        
        IRepo<T> Repository<T>() where T : class;
        Task<int> SaveAsync();
    }
}