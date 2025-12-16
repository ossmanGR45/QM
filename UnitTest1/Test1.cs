using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QM.DataAccess.Data;
using QM.DataAccess.Managers;
using QM.DataAccess.Repo;
using QM.Models;
using QM.Models.Mapping;
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
            var unitOfWork = UnitOfWork.GetInstance();
            var manager = new Manager<Responsible>(unitOfWork);

            var responsibleEntities = new List<Responsible>
            {
                // First Item
                new Responsible
                {
                    EntityName = "Department A",
                    ContactEmail = "mohammed@gmail.com",
                    ContactName = "Mohammed Ali",
                    ContactPhoneNumber = "1234567890"
                }, // <--- Note the comma here!

                // Second Item
                new Responsible
                {
                    EntityName = "Department B",
                    ContactEmail = "sarah@gmail.com",
                    ContactName = "Sarah Smith",
                    ContactPhoneNumber = "0987654321"
                }
            };

            await manager.AddUpdateAsync(responsibleEntities);
            await unitOfWork.SaveAsync();

            Console.WriteLine(responsibleEntities.First().Id);
        }

        [TestMethod]
        public async Task AddAction()
        {
            var unitOfWork = UnitOfWork.GetInstance();
            var manager = new Manager<Actions>(unitOfWork);


            var action = new Actions
            {
                ActionName = "Test Action",
                ActionDescription = "This is a test action",
                ActionType = QM.Models.Enums.ActionType.Avoidance,

            };


            await manager.AddUpdateAsync(action);
            await unitOfWork.SaveAsync();

            Console.WriteLine(action.Id);
        }

        [TestMethod]
        public async Task AddCategory()
        {
            var unitOfWork = UnitOfWork.GetInstance();
            var manager = new Manager<Category>(unitOfWork);



            var category = new Category
            {
                CategoryName = "Test Category",

            };


            await manager.AddUpdateAsync(category);
            await unitOfWork.SaveAsync();

            Console.WriteLine(category.Id);
        }

        [TestMethod]

        public async Task AddCause()
        {
            var unitOfWork = UnitOfWork.GetInstance();
            var manager = new Manager<Cause>(unitOfWork);

            var cause = new Cause
            {
                CauseDescription = "Test Cause"
            };

            await manager.AddUpdateAsync(cause);
            await unitOfWork.SaveAsync();

            Console.WriteLine(cause.Id);
        }

        [TestMethod]
        public async Task AddWorkEntity()
        {
            var unitOfWork = UnitOfWork.GetInstance();
            var manager = new Manager<WorkEntity>(unitOfWork);

            var workEntity = new WorkEntity
            {
                Name = "Test Work Entity",
            };

            await manager.AddUpdateAsync(workEntity);
            await unitOfWork.SaveAsync();

            Console.WriteLine(workEntity.Id);
        }

        [TestMethod]
        public async Task AddRequest()
        {
            var unitOfWork = UnitOfWork.GetInstance();
            var manager = new Manager<Request>(unitOfWork);

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

            await manager.AddUpdateAsync(request);
            await unitOfWork.SaveAsync();

            Console.WriteLine(request.Id);
        }

        [TestMethod]
        public async Task AddRisk()
        {
            var unitOfWork = UnitOfWork.GetInstance();
            var manager = new Manager<Risk>(unitOfWork);
            var managerCategory = new Manager<Category>(unitOfWork);

            var categories = await managerCategory.FindAllAsync(r => r.CategoryName == "Test Category");
            var category = categories.FirstOrDefault();

            var risk = new Risk
            {
                RiskName = "Test Risk",
                RiskDescription = "This is a test risk",
                Location = "Test Location",
                likelihood = QM.Models.Enums.Likelihood.Medium,
                Impact = QM.Models.Enums.Impact.High,
                Category = category,
            };

            await manager.AddUpdateAsync(risk);
            await unitOfWork.SaveAsync();

            Console.WriteLine(risk.Id);
        }

        [TestMethod]
        public async Task AddStrategicGoal()
        {
            var unitOfWork = UnitOfWork.GetInstance();
            var manager = new Manager<StrategicGoal>(unitOfWork);

            var strategicGoal = new StrategicGoal
            {
                GoalReference = "Goal 1",
                GoalDescription = "Test Strategic Goal"
            };

            await manager.AddUpdateAsync(strategicGoal);
            await unitOfWork.SaveAsync();

            Console.WriteLine(strategicGoal.Id);
        }




        // maaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaappppppppppppppppppppppppppppiiiiiiiiiiiiiiiiiiiiiiiiiiiinnnnnnnnnnnnnnggggggggggggggg
        // maaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaappppppppppppppppppppppppppppiiiiiiiiiiiiiiiiiiiiiiiiiiiinnnnnnnnnnnnnnggggggggggggggg
        // maaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaappppppppppppppppppppppppppppiiiiiiiiiiiiiiiiiiiiiiiiiiiinnnnnnnnnnnnnnggggggggggggggg
        // maaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaappppppppppppppppppppppppppppiiiiiiiiiiiiiiiiiiiiiiiiiiiinnnnnnnnnnnnnnggggggggggggggg


        [TestMethod]
        public async Task AddRiskCauseMapping()
        {
            var unitOfWork = UnitOfWork.GetInstance();
            var manager = new Manager<RiskCauseMapping>(unitOfWork);
            var managerRisk = new Manager<Risk>(unitOfWork);
            var managerCause = new Manager<Cause>(unitOfWork);

            var risks = await managerRisk.FindAllAsync(r => r.RiskName == "Test Risk");
            var risk = risks.FirstOrDefault();

            var causes = await managerCause.FindAllAsync(c => c.CauseDescription == "Test Cause");
            var cause = causes.FirstOrDefault();

            var riskCauseMapping = new RiskCauseMapping
            {
                Risk = risk,
                Cause = cause
            };

            await manager.AddUpdateAsync(riskCauseMapping);
            await unitOfWork.SaveAsync();

            Console.WriteLine(riskCauseMapping.Id);


        }

        [TestMethod]
        public async Task AddActionResponsibleMapping()
        {
            var unitOfWork = UnitOfWork.GetInstance();
            var manager = new Manager<ActionResponsibleMapping>(unitOfWork);
            var managerAction = new Manager<Actions>(unitOfWork);
            var managerResponsible = new Manager<Responsible>(unitOfWork);

            var actions = await managerAction.FindAllAsync(a => a.ActionName == "Test Action");
            var action = actions.FirstOrDefault();

            var responsibles = await managerResponsible.FindAllAsync(r => r.EntityName == "Department A");
            var responsible = responsibles.FirstOrDefault();

            var actionResponsibleMapping = new ActionResponsibleMapping
            {
                Action = action,
                Responsible = responsible
            };

            await manager.AddUpdateAsync(actionResponsibleMapping);
            await unitOfWork.SaveAsync();

            Console.WriteLine(actionResponsibleMapping.Id);


        }

        [TestMethod]
        public async Task AddRequestActionMapping()
        {
            var unitOfWork = UnitOfWork.GetInstance();
            var manager = new Manager<RequestActionMapping>(unitOfWork);
            var managerRequest = new Manager<Request>(unitOfWork);
            var managerAction = new Manager<Actions>(unitOfWork);

            var requests = await managerRequest.FindAllAsync(r => r.Description == "This is");
            var request = requests.FirstOrDefault();

            var actions = await managerAction.FindAllAsync(a => a.ActionName == "Test Action");
            var action = actions.FirstOrDefault();

            var requestActionMapping = new RequestActionMapping
            {
                Request = request,
                Action = action
            };

            await manager.AddUpdateAsync(requestActionMapping);
            await unitOfWork.SaveAsync();

            Console.WriteLine(requestActionMapping.Id);
        }

        [TestMethod]
        public async Task AddRequestRiskMapping()
        {
            var unitOfWork = UnitOfWork.GetInstance();
            var manager = new Manager<RequestRiskMapping>(unitOfWork);
            var managerRequest = new Manager<Request>(unitOfWork);
            var managerRisk = new Manager<Risk>(unitOfWork);

            var requests = await managerRequest.FindAllAsync(r => r.Description == "This is");
            var request = requests.FirstOrDefault();

            var risks = await managerRisk.FindAllAsync(r => r.RiskName == "Test Risk");
            var risk = risks.FirstOrDefault();

            var requestRiskMapping = new RequestRiskMapping
            {
                Request = request,
                Risk = risk
            };

            await manager.AddUpdateAsync(requestRiskMapping);
            await unitOfWork.SaveAsync();

            Console.WriteLine(requestRiskMapping.Id);
        }

        [TestMethod]
        public async Task AddRiskActionMapping()
        {
            var unitOfWork = UnitOfWork.GetInstance();
            var manager = new Manager<RiskActionMapping>(unitOfWork);
            var managerRisk = new Manager<Risk>(unitOfWork);
            var managerAction = new Manager<Actions>(unitOfWork);

            var risks = await managerRisk.FindAllAsync(r => r.RiskName == "Test Risk");
            var risk = risks.FirstOrDefault();

            var actions = await managerAction.FindAllAsync(a => a.ActionName == "Test Action");
            var action = actions.FirstOrDefault();

            var riskActionMapping = new RiskActionMapping
            {
                Risk = risk,
                Action = action
            };

            await manager.AddUpdateAsync(riskActionMapping);
            await unitOfWork.SaveAsync();

            Console.WriteLine(riskActionMapping.Id);
        }

        [TestMethod]
        public async Task AddActionCauseMapping()
        {
            var unitOfWork = UnitOfWork.GetInstance();
            var manager = new Manager<ActionCauseMapping>(unitOfWork);
            var managerAction = new Manager<Actions>(unitOfWork);
            var managerCause = new Manager<Cause>(unitOfWork);

            var actions = await managerAction.FindAllAsync(a => a.ActionName == "Test Action");
            var action = actions.FirstOrDefault();

            var causes = await managerCause.FindAllAsync(c => c.CauseDescription == "Test Cause");
            var cause = causes.FirstOrDefault();

            var actionCauseMapping = new ActionCauseMapping
            {
                Action = action,
                Cause = cause
            };

            await manager.AddUpdateAsync(actionCauseMapping);
            await unitOfWork.SaveAsync();

            Console.WriteLine(actionCauseMapping.Id);


        }

        [TestMethod]
        public async Task AddRiskStrategicGoalMapping()
        {
            var unitOfWork = UnitOfWork.GetInstance();
            var manager = new Manager<RiskStrategicGoalMapping>(unitOfWork);
            var managerRisk = new Manager<Risk>(unitOfWork);
            var managerStrategicGoal = new Manager<StrategicGoal>(unitOfWork);

            var risks = await managerRisk.FindAllAsync(r => r.RiskName == "Test Risk");
            var risk = risks.FirstOrDefault();

            var strategicGoals = await managerStrategicGoal.FindAllAsync(sg => sg.GoalReference == "Goal 1");
            var strategicGoal = strategicGoals.FirstOrDefault();

            var riskStrategicGoalMapping = new RiskStrategicGoalMapping
            {
                Risk = risk,
                StrategicGoal = strategicGoal
            };

            await manager.AddUpdateAsync(riskStrategicGoalMapping);
            await unitOfWork.SaveAsync();

            Console.WriteLine(riskStrategicGoalMapping.Id);

        }

        [TestMethod]
        public async Task AddRequestResponsibleMapping()
        {
            var unitOfWork = UnitOfWork.GetInstance();
            var manager = new Manager<RequestResponsibleMapping>(unitOfWork);
            var managerRequest = new Manager<Request>(unitOfWork);
            var managerResponsible = new Manager<Responsible>(unitOfWork);
            var requests = await managerRequest.FindAllAsync(r => r.Description == "This is");
            var request = requests.FirstOrDefault();
            var responsibles = await managerResponsible.FindAllAsync(r => r.EntityName == "Department A");
            var responsible = responsibles.FirstOrDefault();
            var requestResponsibleMapping = new RequestResponsibleMapping
            {
                Request = request,
                Responsible = responsible
            };
            await manager.AddUpdateAsync(requestResponsibleMapping);
            await unitOfWork.SaveAsync();
            Console.WriteLine(requestResponsibleMapping.Id);
        }

        [TestMethod]
        public async Task AddRequestCauseMapping()
        {
            var unitOfWork = UnitOfWork.GetInstance();
            var manager = new Manager<RequestCauseMapping>(unitOfWork);
            var managerRequest = new Manager<Request>(unitOfWork);
            var managerCause = new Manager<Cause>(unitOfWork);

            var requests = await managerRequest.FindAllAsync(r => r.Description == "This is");
            var request = requests.FirstOrDefault();

            var causes = await managerCause.FindAllAsync(c => c.CauseDescription == "Test Cause");
            var cause = causes.FirstOrDefault();

            var requestCauseMapping = new RequestCauseMapping
            {
                Request = request,
                Cause = cause
            };

            await manager.AddUpdateAsync(requestCauseMapping);
            await unitOfWork.SaveAsync();

            Console.WriteLine(requestCauseMapping.Id);
        }



    }
}
