using LinqKit;
using Microsoft.AspNetCore.Mvc;
using QM.DataAccess.Managers;
using QM.DataAccess.Repo;
using QM.DataAccess.Repo.IRepo;
using QM.Models.DataModels;
using QM.Models.DTO;
using QM.Models.Mapping;
using QM.Utility;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using static QM.Models.Enums;

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
            string? categoryName = null,
            int? status = null,
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

            if (!string.IsNullOrEmpty(categoryName))
                filter = filter.And(r => r.CategoryName.Contains(categoryName));

            if (status.HasValue)
                filter = filter.And(r => (int)r.Status == status);

            var _manager = new Manager<Risk>(_uow);
            var records = await _manager.FindAllAsync(filter, orderBy, paggerBy, include?.ToStringList());

            return Ok(records);
        }


        [HttpPost("addUpdate")]
        public async Task<IActionResult> AddUpdateRisk([FromBody] RiskDto dto)
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

            // 1. Initialize Managers
            var riskManager = new Manager<Risk>(_uow);
            var notificationManager = new Manager<NotificationModel>(_uow);
            var mappingManager = new Manager<RiskActionMapping>(_uow); // You'd need similar for Causes/Goals

            // 2. Fetch existing risk with its current "Mappings" included
            // Note: Use the 'include' list parameter your Manager supports
            var includeList = new List<string> { "RiskActions", "RiskCauses", "RiskGoals" };
            var risk = dto.Id > 0 ? await riskManager.GetByIdAsync(dto.Id.Value, includeList) : null;

            bool isNew = (risk == null);

            if (isNew)
            {
                risk = new Risk();
            }
            else
            {
                var context = _uow.GetContext();
                
                risk.RiskActions.Clear(); // This will mark existing mappings for deletion

                risk.RiskCauses.Clear();

                risk.RiskGoals.Clear();
                // ... repeat for RiskCauses and RiskGoals ...
            }

            // 3. Map DTO to the Entity
            risk.RiskName = dto.RiskName;
            risk.Department = dto.Department;
            risk.RiskDescription = dto.RiskDescription;
            risk.Location = dto.Location;
            risk.likelihood = dto.likelihood;
            risk.Impact = dto.Impact;
            risk.ResponsibleId = dto.ResponsibleId;
            risk.CategoryName = dto.CategoryName;
            risk.Status = dto.Status;
            risk.UserId = userId;
            risk.Custom = (userRole == "Admin") ? false: true ;
            risk.ReDirected = (userRole == "Admin" || userRole == "Initi") ? false:true;


            // 4. Re-map the Many-to-Many lists from your DTO
            foreach (var actionDto in dto.Actions)
            {
                var mapping = new RiskActionMapping();

                if (actionDto.Id == null || actionDto.Id == 0)
                {
                    // USER ADDED A NEW ACTION MANUALLY
                    mapping.Action = new Actions
                    {
                        ActionDescription = actionDto.ActionDescription,
                        ActionType = actionDto.ActionType,
                        Custom = true // This is key!
                    };
                }
                else
                {
                    // USER SELECTED AN EXISTING ACTION FROM THE LIST
                    mapping.ActionID = actionDto.Id.Value;
                }

                risk.RiskActions.Add(mapping);
            }

            foreach (var causeDto in dto.Causes)
            {
                var mapping = new RiskCauseMapping();

                if (causeDto.Id == null || causeDto.Id == 0)
                {
                    // USER ADDED A NEW CAUSE MANUALLY
                    mapping.Cause = new Cause
                    {
                        // Use the property names from your Cause model
                        CauseDescription = causeDto.Description,
                        Custom = true
                    };
                }
                else
                {
                    // USER SELECTED AN EXISTING CAUSE FROM THE SYSTEM
                    mapping.CauseID = causeDto.Id.Value;
                }

                risk.RiskCauses.Add(mapping);
            }


            if (dto.StrategicGoals != null)
            {
                foreach (var goalDto in dto.StrategicGoals)
                {
                    // We only care about the ID because the Goal already exists in the DB
                    var mapping = new RiskStrategicGoalMapping
                    {
                        StrategicGoalId = goalDto.Id.Value
                    };

                    risk.RiskGoals.Add(mapping);
                }
            }


            // 5. Use your Manager to save
            await riskManager.AddUpdateAsync(risk);
           

            // 6. --- YOUR LATEST NOTIFICATION UPDATE ---
            // Create the notification record using your NotificationModel


            
            var notify = new NotificationModel
            {
                requestId = risk.Id, // Link to the Risk
                UserId = userId,
                requestType = 0,
                createdAt = DateTime.Now
            };

            if (isNew || risk.ReDirected == true)
            {
                notify.status = notificationType.created;
            }
            else if (risk.Status == RequestStatus.Accepted)
            {
                notify.status = notificationType.accept;
            }
            else if (risk.Status == RequestStatus.Rejected)
            {
                notify.status = notificationType.reject;
            }
            else
            {
                notify.status = notificationType.updated;
            }

            await notificationManager.AddUpdateAsync(notify);
            await _uow.SaveChangesAsync();
            

            return Ok(risk);
        }

    }
}