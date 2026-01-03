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
    [Route("api/log")]
    [ApiController]
    public class LogsController : BaseController
    {
        public LogsController(IUnitOfWork uow) : base(uow) { }

        [HttpGet]
        public async Task<IActionResult> GetLogs(
            int? id = null,
            DateTime? date = null,
            bool? occurded = null,
            string? category = null,
            string? riskName = null,
            int? likelihood = null,
            int? impact = null,
            string? responsible = null,
            string? description = null,
            string? report = null,
            string? postLikelihood = null,
            string? postImpact = null,
            string? orderBy = null,
            Pagger? paggerBy = null,
            string? include = null)
        {
            var filter = PredicateBuilder.New<Log>(true);

            if (id.HasValue)
                filter = filter.And(l => l.Id == id);

            if(date.HasValue)
                filter = filter.And(l => l.Year == date);

            if (occurded.HasValue)
                filter = filter.And(l => l.Occured == occurded);

            if (!string.IsNullOrEmpty(category))
                filter = filter.And(l => l.Category.Contains(category));

            if (!string.IsNullOrEmpty(riskName))
                filter = filter.And(l => l.RiskName.Contains(riskName));

            if (likelihood.HasValue)
                filter = filter.And(l => (int)l.Likelihood == likelihood);

            if (impact.HasValue)
                filter = filter.And(l => (int)l.Impact == impact);

            if (!string.IsNullOrEmpty(responsible))
                filter = filter.And(l => l.Responsible.Contains(responsible));

            if (!string.IsNullOrEmpty(description))
                filter = filter.And(l => l.Description.Contains(description));

            if (!string.IsNullOrEmpty(report))
                filter = filter.And(l => l.report != null && l.report.Contains(report));

            if (!string.IsNullOrEmpty(postLikelihood))
                filter = filter.And(l => l.PostLikelihood != null && ((int)l.PostLikelihood == int.Parse(postLikelihood)));

            if (!string.IsNullOrEmpty(postImpact))
                filter = filter.And(l => l.PostImpact != null && ((int)l.PostImpact == int.Parse(postImpact)));


            var _manager = new Manager<Log>(_uow);
            var records = await _manager.FindAllAsync(filter, orderBy, paggerBy, include?.ToStringList());

            return Ok(records);
        }
    }
}