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
    [Route("api/responsible")]
    [ApiController]
    public class ResponsiblesController : BaseController
    {
        public ResponsiblesController(IUnitOfWork uow) : base(uow) { }

        [HttpGet]
        public async Task<IActionResult> GetResponsibles(
            int? id = null,
            string? entityName = null,
            string? contactName = null,
            string? contactEmail = null,
            string? phoneNumber = null,
            string? orderBy = null,
            Pagger? paggerBy = null,
            string? include = null)
        {
            var filter = PredicateBuilder.New<Responsible>(true);

            if (id.HasValue)
                filter = filter.And(r => r.Id == id);

            if (!string.IsNullOrEmpty(entityName))
                filter = filter.And(r => r.EntityName.Contains(entityName));

            if (!string.IsNullOrEmpty(contactName))
                filter = filter.And(r => r.ContactName.Contains(contactName));

            if (!string.IsNullOrEmpty(contactEmail))
                filter = filter.And(r => r.ContactEmail.Contains(contactEmail));

            if (!string.IsNullOrEmpty(phoneNumber))
                filter = filter.And(r => r.ContactPhoneNumber.Contains(phoneNumber));


            var _manager = new Manager<Responsible>(_uow);
            var records = await _manager.FindAllAsync(filter, orderBy, paggerBy, include?.ToStringList());

            return Ok(records);
        }
    }
}