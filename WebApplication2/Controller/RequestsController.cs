using Humanizer;
using LinqKit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QM.DataAccess.Managers;
using QM.DataAccess.Repo;
using QM.DataAccess.Repo.IRepo;
using QM.Models.DataModels;
using QM.Models.DTO;
using QM.Models.Mapping;
using QM.Utility;
using System.Linq.Expressions;
using System.Security.Claims;
using static QM.Models.Enums;

namespace QM.Controller
{
    [Route("api/request")]
    [ApiController]
    public class RequestsController : BaseController
    {
        public RequestsController(IUnitOfWork uow) : base(uow) { }

        [HttpGet]
        public async Task<IActionResult> GetRequests(
            int? id = null,
            DateTime? year = null,
            int? likelihood = null,
            int? impact = null,
            DateTime? expectedTime = null,
            int? responsibleId = null,
            string? description = null,
            int? status = null,
            bool? occured = null,
            string? orderBy = null,
            Pagger? paggerBy = null,
            string? include = null)
        {
            var filter = PredicateBuilder.New<Request>(true);

            if (id.HasValue)
                filter = filter.And(r => r.Id == id);

            if (year.HasValue)
                filter = filter.And(r => r.Year == year);

            if (likelihood.HasValue)
                filter = filter.And(r => (int)r.Likelihood == likelihood);

            if (impact.HasValue)
                filter = filter.And(r => (int)r.Impact == impact);

            if (expectedTime.HasValue)
                filter = filter.And(r => r.ExpectedTime == expectedTime);

            if (responsibleId.HasValue)
                filter = filter.And(r => r.ResponsibleId == responsibleId);

            if (!string.IsNullOrEmpty(description))
                filter = filter.And(r => r.Description.Contains(description));

            if (status.HasValue)
                filter = filter.And(r => (int)r.Status == status);

            if (occured.HasValue)
                filter = filter.And(r => r.Occured == occured);


            var _manager = new Manager<Request>(_uow);
            var records = await _manager.FindAllAsync(filter, orderBy, paggerBy, include?.ToStringList());

            return Ok(records);
        }





        [Authorize(Roles = "Initiator,Risk Manager,Admin")]
        [HttpPost("addUpdate")]
        public async Task<IActionResult> AddUpdate([FromBody] RequestDto dto)
        {
            // Retrieve the Name Identifier (often the User ID)
            var stringUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(stringUserId))
            {
                return Unauthorized("User isn't logged in.");
            }

            if (!int.TryParse(stringUserId, out int userId))
            {
                return BadRequest("Invalid User ID in token.");
            }

            // Retrieve the Role
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (dto == null) return BadRequest("Data is null");

            var _manager = new Manager<Request>(_uow);
            Request request;
            bool isNew = (dto.Id == 0);

            // 1. Logic for Add vs Update
            if (isNew)
            {
                request = new Request { Status = RequestStatus.InProgress };
                await _manager.AddUpdateAsync(request); // Marks for insertion
            }
            else
            {
                // Load with Actions included so we can Clear() them safely
                request = await _manager.GetByIdAsync(dto.Id, new List<string> { "RequestActions" });
                if (request == null) return NotFound();
            }

            // 2. Map basic data
            request.Description = dto.Description;
            request.Department = dto.Department;
            request.Likelihood = dto.Likelihood;
            request.Impact = dto.Impact;
            request.PostLikelihood = dto.PostLikelihood;
            request.PostImpact = dto.PostImpact;
            request.UserId = userId;
            request.ResponsibleId = dto.ResponsibleId;
            request.RiskId = dto.RiskId;
            request.ReDirected = (userRole == "Admin" || userRole == "Initi") ? false : true;

            // 3. Sync Actions (Automatic ID linking)
            request.RequestActions?.Clear();
            foreach (var actionDto in dto.Actions)
            {
                request.RequestActions.Add(new RequestActionMapping
                {
                    ActionID = actionDto.Id ?? 0,
                    Action = (actionDto.Id == 0 || actionDto.Id == null) ? new Actions
                    {
                        ActionDescription = actionDto.ActionDescription,
                        ActionType = (ActionType)actionDto.ActionType,
                        Custom = true
                    } : null
                });
            }

            // 4. CALL THE STATUS & NOTIFICATION FUNCTION
            await ProcessStatusAndNotifications(request, dto, isNew);

            // 5. THE SAVE POINT (Everything fails if this fails)
            await _uow.SaveChangesAsync();

            return Ok("succefull");
        }

        private async Task ProcessStatusAndNotifications(Request request, RequestDto dto, bool isNew)
        {
            // Retrieve the Name Identifier (often the User ID)
            var stringUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;


            int userId = int.Parse(stringUserId);


            // Retrieve the Role
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            notificationType finalNotifType = notificationType.updated;

            if (isNew || userRole == "Risk Manager")
            {
                finalNotifType = notificationType.created;
            }
            else if (dto.Status.HasValue)
            {
                request.Status = dto.Status.Value;
                request.rejectReason = dto.rejectReason;

                // Map your enum values
                if (dto.Status == RequestStatus.Accepted) finalNotifType = notificationType.accept;
                else if (dto.Status == RequestStatus.Rejected) finalNotifType = notificationType.reject;
            }

            // Create the Notification record using your specialized model
            var _notifManager = new Manager<NotificationModel>(_uow);
            await _notifManager.AddUpdateAsync(new NotificationModel
            {
                requestId = request.Id,
                // If new, notify the Responsible party. If update, notify the User (creator).
                UserId = userId,
                status = finalNotifType,
                requestType = requestType.Incident,
                createdAt = DateTime.Now
            });

            
        }


    }
}