using QM.DataAccess.Data;
using System;
using System.Threading.Tasks;

namespace QM.DataAccess.Repo.IRepo
{
    public interface IUnitOfWork 
    {
        
        Task<int> SaveAsync();
    }
}