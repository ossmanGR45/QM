using LinqKit;
using Microsoft.AspNetCore.Mvc;
using QM.DataAccess.Managers;
using QM.DataAccess.Repo;
using QM.DataAccess.Repo.IRepo;
using QM.Models.DataModels;
using QM.Utility;
using static QM.Models.Enums;

namespace QM.Controller // Use your controller namespace
{
    [Route("api/notification")]
    [ApiController]
    public class NotificationsController : BaseController
    {
        public NotificationsController(IUnitOfWork uow) : base(uow) { }

        [HttpGet]
        public async Task<IActionResult> GetNotifications(
            int? id = null,
            int? userId = null,
            int? requestId = null,
            notificationType? status = null, 
            requestType? requestType = null, 
            string? orderBy = null,
            Pagger? paggerBy = null,
            string? include = null)
        {
            // 1. Initialize the Predicate for NotificationModel
            var filter = PredicateBuilder.New<NotificationModel>(true);

            // 2. Apply Filters
            if (id.HasValue)
                filter = filter.And(n => n.Id == id);

            if (userId.HasValue)
                filter = filter.And(n => n.UserId == userId);

            if (requestId.HasValue)
                filter = filter.And(n => n.requestId == requestId);

            if (status.HasValue)
                filter = filter.And(n => n.status == status);

            if (requestType.HasValue)
                filter = filter.And(n => n.requestType == requestType);

            // 3. Use your Manager pattern to fetch records
            var _manager = new Manager<NotificationModel>(_uow);
            var records = await _manager.FindAllAsync(filter, orderBy, paggerBy, include?.ToStringList());

            return Ok(records);
        }
    }
}