using Microsoft.EntityFrameworkCore;
using QM.DataAccess.Data;
using QM.DataAccess.DbInitializer.IDbInitializer;
using QM.DataAccess.Repo.IRepo;
using QM.Models;


public class DbInitializer : IDbInitializer
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ApplicationDbContext _db;

    public DbInitializer(IUnitOfWork unitOfWork, ApplicationDbContext db)
    {
        _unitOfWork = unitOfWork;
        _db = db;
    }

    public void Initialize()
    {
        // 1. Ensure Migrations
        try { if (_db.Database.GetPendingMigrations().Any()) _db.Database.Migrate(); }
        catch (Exception) { }

        // 2. Call your individual seeders in order
        // (Order matters! Seed Categories before Products if Products need a CategoryId)
        SeedCategories();
        SeedActions();
        SeedCause();
        SeedEntity();
        SeedRequest();
        SeedRisk();
        SeedStrategicGoal();

        _unitOfWork.SaveAsync().GetAwaiter().GetResult();

    }

    private void SeedCategories()
    {
        // Check if data exists
        if (_unitOfWork.Repository<Category>().FindAllAsync().GetAwaiter().GetResult().Any()) return;

        var categories = new List<Category>
        {
            new Category { CategoryName = "Electronics" },

        };

        _unitOfWork.Repository<Category>().AddUpdateAsync(categories).GetAwaiter().GetResult();
    }

    private void SeedActions()
    {
        if (_unitOfWork.Repository<Actions>().FindAllAsync().GetAwaiter().GetResult().Any()) return;

        // NOTE: We rely on the Categories being saved first so the IDs exist
        var actions = new List<Actions>
        {
            new Actions { ActionDescription = "action description test", ActionType = Enums.ActionType.Avoidance , ResponsibleEntityID = 0 }, // Assuming ID 1 exists
            
        };

        _unitOfWork.Repository<Actions>().AddUpdateAsync(actions).GetAwaiter().GetResult();
    }

    private void SeedCause()
    {
        if (_unitOfWork.Repository<Cause>().FindAllAsync().GetAwaiter().GetResult().Any()) return;

        // NOTE: We rely on the Categories being saved first so the IDs exist
        var cause = new List<Cause>
        {
            new Cause { CauseDescription = "cause description test" }, // Assuming ID 1 exists
            
        };

        _unitOfWork.Repository<Cause>().AddUpdateAsync(cause).GetAwaiter().GetResult();
        
    }

    private void SeedEntity()
    {
        if (_unitOfWork.Repository<Entity>().FindAllAsync().GetAwaiter().GetResult().Any()) return;

        // NOTE: We rely on the Categories being saved first so the IDs exist
        var entity = new List<Entity>
        {
            new Entity { EntityName = "مكتب الجامعة", ContactEmail = "mohammedarqan@gmail.com", ContactName ="mohammed arqan", ContactPhoneNumber="0775760392" }, // Assuming ID 1 exists
            
        };

        _unitOfWork.Repository<Entity>().AddUpdateAsync(entity).GetAwaiter().GetResult();
        

    }

    private void SeedRequest()
    {
        if (_unitOfWork.Repository<Request>().FindAllAsync().GetAwaiter().GetResult().Any()) return;

        // NOTE: We rely on the Categories being saved first so the IDs exist
        var request = new List<Request>
        {
            new Request { Year = new DateTime(2025,10,10), Before = true, Likelihood = Enums.Likelihood.veryLow, Impact = Enums.Impact.veryLow ,ExpectedTime = new DateTime(2025,11,1), Responsible = "othman", Description = "Request description test", Status = Enums.RequestStatus.InProgress, OutcomeSummary = "outcumSummary test", ImpactAfter= Enums.Impact.veryLow, LikelihoodAfter = Enums.Likelihood.veryLow, Occured = true}, // Assuming ID 1 exists
            
        };

        _unitOfWork.Repository<Request>().AddUpdateAsync(request).GetAwaiter().GetResult();
        

    }

    private void SeedRisk()
    {
        if (_unitOfWork.Repository<Risk>().FindAllAsync().GetAwaiter().GetResult().Any()) return;

        // NOTE: We rely on the Categories being saved first so the IDs exist
        var risk = new List<Risk>
        {
            new Risk { RiskName = "Riskname test1", RiskDescription = "Risk description test", Location = "IT building", CategoryID = 0, likelihood = Enums.Likelihood.veryLow, Impact = Enums.Impact.veryLow }, // Assuming ID 1 exists
            
        };

        _unitOfWork.Repository<Risk>().AddUpdateAsync(risk).GetAwaiter().GetResult();
        

    }

    private void SeedStrategicGoal()
    {
        if (_unitOfWork.Repository<StrategicGoal>().FindAllAsync().GetAwaiter().GetResult().Any()) return;

        // NOTE: We rely on the Categories being saved first so the IDs exist
        var strategicGoal = new List<StrategicGoal>
        {
            new StrategicGoal { GoalReference = "الغاية السابعة", GoalDescription = "نظام وطبيعة",  }, // Assuming ID 1 exists
            
        };

        _unitOfWork.Repository<StrategicGoal>().AddUpdateAsync(strategicGoal).GetAwaiter().GetResult();
        
    }






}