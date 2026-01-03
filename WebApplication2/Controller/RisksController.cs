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
    [Route("api/risk")]
    [ApiController]
    public class RisksController : BaseController
    {
        public RisksController(IUnitOfWork uow) : base(uow) { }

        [HttpGet]
        public async Task<IActionResult> GetRisks(
            int? id = null,
            string? name = null,
            string? description = null,
            string? location = null,
            int? likelihood = null,
            int? impact = null,
            int? categoryId = null,
            string? orderBy = null,
            Pagger? paggerBy = null,
            string? include = null)
        {
            var filter = PredicateBuilder.New<Risk>(true);

            if (id.HasValue)
                filter = filter.And(r => r.Id == id);

            if (!string.IsNullOrEmpty(name))
                filter = filter.And(r => r.RiskName.Contains(name));

            if (!string.IsNullOrEmpty(description))
                filter = filter.And(r => r.RiskDescription.Contains(description));

            if (!string.IsNullOrEmpty(location))
                filter = filter.And(r => r.Location.Contains(location));

            if (likelihood.HasValue)
                filter = filter.And(r => (int)r.likelihood == likelihood);

            if (impact.HasValue)
                filter = filter.And(r => (int)r.Impact == impact);

            if (categoryId.HasValue)
                filter = filter.And(r => r.CategoryID == categoryId);

            var _manager = new Manager<Risk>(_uow);
            var records = await _manager.FindAllAsync(filter, orderBy, paggerBy, include?.ToStringList());

            return Ok(records);
        }


        [HttpPost("addUpdate")]
        public async Task<IActionResult> AddUpdateRisk([FromBody] Risk risk)
        {
            if (risk == null)
                return BadRequest("Risk data is null.");

            var _manager = new Manager<Risk>(_uow);
            var createdRisk = await _manager.AddUpdateAsync(risk);
            return CreatedAtAction(nameof(GetRisks), new { id = createdRisk.Id }, createdRisk);

        }

    }
}