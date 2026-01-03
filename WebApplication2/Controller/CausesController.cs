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
    [Route("api/cause")]
    [ApiController]
    public class CausesController : BaseController
    {
        public CausesController(IUnitOfWork uow) : base(uow) { }

        [HttpGet]
        public async Task<IActionResult> GetCauses(
            int? id = null,
            string? description = null,
            string? orderBy = null,
            Pagger? paggerBy = null,
            string? include = null)
        {
            var filter = PredicateBuilder.New<Cause>(true);

            if (id.HasValue)
                filter = filter.And(c => c.Id == id);

            if (!string.IsNullOrEmpty(description))
                filter = filter.And(c => c.CauseDescription.Contains(description));

            var _manager = new Manager<Cause>(_uow);
            var records = await _manager.FindAllAsync(filter, orderBy, paggerBy, include?.ToStringList());

            return Ok(records);
        }
    }
}