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


    }


}