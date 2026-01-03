using LinqKit;
using Microsoft.AspNetCore.Mvc;
using QM.DataAccess.Managers;
using QM.DataAccess.Repo;
using QM.DataAccess.Repo.IRepo;
using QM.Models.DataModels;
using QM.Utility;
using System.Linq.Expressions;

namespace QM.Controller
{
    [Route("api/workentity")]
    [ApiController]
    public class WorkEntitiesController : BaseController
    {
        public WorkEntitiesController(IUnitOfWork uow) : base(uow) { }

        [HttpGet]
        public async Task<IActionResult> GetWorkEntities(
            int? id = null,
            string? name = null,
            string? orderBy = null,
            Pagger? paggerBy = null,
            string? include = null)
        {
            var filter = PredicateBuilder.New<WorkEntity>(true);

            if (id.HasValue)
                filter = filter.And(w => w.Id == id);

            if (!string.IsNullOrEmpty(name))
                filter = filter.And(w => w.Name.Contains(name));


            var _manager = new Manager<WorkEntity>(_uow);
            var records = await _manager.FindAllAsync(filter, orderBy, paggerBy, include?.ToStringList());

            return Ok(records);
        }
    }
}