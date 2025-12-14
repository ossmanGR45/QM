using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        

        private UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            
        }



        public static UnitOfWork getInstance()
        {


            string connectionString = "Server=localhost\\SQLEXPRESS;Database=QM;Trusted_Connection=true;TrustServerCertificate=true";

            
            var options = new DbContextOptionsBuilder<ApplicationDbContext>() 
                .UseSqlServer(connectionString)
                .Options;

            // 3. Create the context using the options
            using var context = new ApplicationDbContext(options);

            // Optional: Ensure the database/tables exist (Use with caution!)
            // context.Database.EnsureCreated(); 

            var unitOfWork = new UnitOfWork(context);



            return unitOfWork;
        }

        // The "Best Practice" way to get a Generic Repo without hardcoding properties
       

        public async Task<int> SaveAsync()
        {
            return await _db.SaveChangesAsync();
        }
    }
}