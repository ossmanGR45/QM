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
    [Route("api/strategicgoal")]
    [ApiController]
    public class StrategicGoalsController : BaseController
    {
        public StrategicGoalsController(IUnitOfWork uow) : base(uow) { }

        [HttpGet]
        public async Task<IActionResult> GetStrategicGoals(
            int? id = null,
            string? reference = null,
            string? description = null,
            string? orderBy = null,
            Pagger? paggerBy = null,
            string? include = null)
        {
            var filter = PredicateBuilder.New<StrategicGoal>(true);

            if (id.HasValue)
                filter = filter.And(s => s.Id == id);

            if (!string.IsNullOrEmpty(reference))
                filter = filter.And(s => s.GoalReference.Contains(reference));

            if (!string.IsNullOrEmpty(description))
                filter = filter.And(s => s.GoalDescription.Contains(description));

            var _manager = new Manager<StrategicGoal>(_uow);
            var records = await _manager.FindAllAsync(filter, orderBy, paggerBy, include?.ToStringList());

            return Ok(records);
        }
    }
}