using LinqKit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QM.DataAccess.Data;
using QM.DataAccess.Managers;
using QM.DataAccess.Repo;
using QM.DataAccess.Repo.IRepo;
using QM.Models.DataModels;
using QM.Models.DTO;
using QM.Utility;
using System.Linq.Expressions;
using static QM.Models.Enums;



namespace QM.Controller
{
    [Route("api/action")]
    [ApiController]
    public class ActionsController : BaseController
    {
        protected readonly ApplicationDbContext _context;
        
        public ActionsController(IUnitOfWork uow) : base(uow)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetActions( 
            
            int? id = null,
            string? name = null,
            string? description = null,
            bool? type = null,
            string? orderBy = null,
            Pagger? paggerBy = null,
            String? include = null)
        {

            var filter = PredicateBuilder.New<Actions>(true);     

            if (id.HasValue)
            {
                filter = filter.And(a => a.Id == id);
            }

            if (!string.IsNullOrEmpty(description))
            {
                filter = filter.And(a => a.ActionDescription.Contains(description));
            }

            if (type.HasValue)
            {
                var actionType = type.Value ? ActionType.Reduction : ActionType.Avoidance;
                filter = filter.And(a => a.ActionType == actionType);
            }

            var _manager = new Manager<Actions>(_uow);
            var records = await _manager.FindAllAsync(filter,orderBy,paggerBy,include?.ToStringList());

            return Ok(records);
        }



    }
}
