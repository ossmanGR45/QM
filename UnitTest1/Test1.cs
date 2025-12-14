using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QM.DataAccess.Data;
using QM.DataAccess.Repo;
using QM.Models;
using System;
using System.Diagnostics.Metrics;


namespace UnitTest1
{
    [TestClass]
    public sealed class Test1
    {

    
        [TestMethod]
        public async Task AddResponsible()
        {

            var unitOfWork = UnitOfWork.getInstance();

            var newEntity = new Responsible
            {
                EntityName = "Test Company",
                ContactName = "Jane Doe",
                ContactEmail = "jane@test.com",
                ContactPhoneNumber = "0790000000"
            };

            await unitOfWork.Repository<Responsible>().AddUpdateAsync(newEntity);
            await unitOfWork.SaveAsync();

            Console.WriteLine(newEntity.Id);
        }

        [TestMethod]
        public async Task AddAction()
        {
            var unitOfWork = UnitOfWork.getInstance();

            string name = "Responsible Person";

            var responsibleEntity = new WorkEntity
            {
                Name = name
            };

            await unitOfWork.Repository<WorkEntity>().AddUpdateAsync(responsibleEntity);
            


            var responsible = (await unitOfWork.Repository<WorkEntity>().FindAllAsync(r => r.Name == name)).FirstOrDefault();

       
            if (responsible == null)
            {
       
                throw new InvalidOperationException("The Responsible entity was not found in the database.");
            }


            var action = new Actions
            {
                ActionName = "Test Action",
                ActionDescription = "This is a test action",
                ActionType = QM.Models.Enums.ActionType.Avoidance,
                Responsible = responsible
                
            };

            
            await unitOfWork.Repository<Actions>().AddUpdateAsync(action);
            await unitOfWork.SaveAsync();

            Console.WriteLine(action.Id);
        }

        [TestMethod]
        public async Task AddCategory()
        {
            var unitOfWork = UnitOfWork.getInstance();



            var category = new Category
            {
                CategoryName = "Test Category",
                
            };


            await unitOfWork.Repository<Category>().AddUpdateAsync(category);
            await unitOfWork.SaveAsync();

            Console.WriteLine(category.Id);
        }

        [TestMethod]

        public async Task AddCause()
        {
            var unitOfWork = UnitOfWork.getInstance();

            var cause = new Cause
            {
                CauseDescription = "Test Cause"
            };

            await unitOfWork.Repository<Cause>().AddUpdateAsync(cause);
            await unitOfWork.SaveAsync();

            Console.WriteLine(cause.Id);
        }

        [TestMethod]
        public async Task AddWorkEntity()
        {
            var unitOfWork = UnitOfWork.getInstance();

            var workEntity = new Responsible
            {
                EntityName = "Test Entity",
                ContactName = "John Smith",
                ContactEmail = "mohmade@gmail.com",
                ContactPhoneNumber = "1234567890"
            };

            await unitOfWork.Repository<Responsible>().AddUpdateAsync(workEntity);
            await unitOfWork.SaveAsync();

            Console.WriteLine(workEntity.Id);
        }

        [TestMethod]
        public async Task AddRequest()
        {
            var unitOfWork = UnitOfWork.getInstance();

            var request = new Request
            {

                Description = "This is",
                Year = DateTime.Now,
                Likelihood = QM.Models.Enums.Likelihood.High,
                Impact = QM.Models.Enums.Impact.Medium,
                ExpectedTime = DateTime.Now.AddDays(30),
                Responsible = "John Doe",
                Status = QM.Models.Enums.RequestStatus.Accepted,
                Occured = false

            };

            await unitOfWork.Repository<Request>().AddUpdateAsync(request);
            await unitOfWork.SaveAsync();

            Console.WriteLine(request.Id);
        }

    }
}
