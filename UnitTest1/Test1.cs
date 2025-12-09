using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using QM.DataAccess.Data;
using QM.DataAccess.Repo;
using QM.Models;


namespace UnitTest1
{
    [TestClass]
    public sealed class Test1
    {

        private static ApplicationDbContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(options);
        }

        [TestMethod]
        public async Task Test_AddEntity_ViaUnitOfWork()
        {
            
            await using var context = CreateInMemoryContext();

            var unitOfWork = new UnitOfWork(context);

            var newEntity = new Entity
            {
                EntityName = "Test Company",
                ContactName = "Jane Doe",
                ContactEmail = "jane@test.com",
                ContactPhoneNumber = "0790000000"
            };

            await unitOfWork.Repository<Entity>().AddUpdateAsync(newEntity);
            await unitOfWork.SaveAsync();
            Assert.AreEqual(1, await context.Entities.CountAsync());
        }
    }
}
