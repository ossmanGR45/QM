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
            string? responsible = null,
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

            if( year.HasValue)
                filter = filter.And(r => r.Year == year);

            if (likelihood.HasValue)
                filter = filter.And(r => (int)r.Likelihood == likelihood);

            if (impact.HasValue)
                filter = filter.And(r => (int)r.Impact == impact);

            if (expectedTime.HasValue)
                filter = filter.And(r => r.ExpectedTime == expectedTime);

            if (!string.IsNullOrEmpty(responsible))
                filter = filter.And(r => r.Responsible.Contains(responsible));

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

        [Authorize(Roles = "initiator,Manager")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateRequest(RequestDto requestDto)
        {
            if(requestDto == null) { 
                return BadRequest("Request cannot be null.");
            }

            var request = new Request
            {
                Id = requestDto.Id,
                WorkEntity = requestDto.WorkEntity,
                Year = requestDto.Year,
                Category = requestDto.Category,
                Likelihood = requestDto.Likelihood,
                Impact = requestDto.Impact,
                ExpectedTime = requestDto.ExpectedTime,
                Responsible = requestDto.Responsible,
                Description = requestDto.Description,
                Status = requestDto.Status,
                Occured = requestDto.Occured,
                report = requestDto.Report
            };

            request.RequestAction = new List<RequestActionMapping>();

            foreach (var actionDto in requestDto.Actions)
            {


                request.RequestAction.Add(new RequestActionMapping
                {

                    Action = (actionDto.Id == null) ? new Actions
                    {
                        ActionDescription = actionDto.Description,
                        ActionType = (ActionType)actionDto.ActionType,
                        Custom = true

                    } : null,
                    ActionID = (actionDto.Id == null) ? 0 : actionDto.Id.Value

                });

            }




            var _manager = new Manager<Request>(_uow);
            var createdRequest = await _manager.AddUpdateAsync(request);
            await _uow.SaveAsync();
            return CreatedAtAction(nameof(GetRequests), new { id = createdRequest.Id }, createdRequest);

        }

        


        [Authorize(Roles = "Manager,Admin")]
        [HttpPost("update")]
        public async Task<IActionResult> UpdateRequest(RequestDto requestDto)
        {

            if (requestDto == null || requestDto.Id == 0)
            {
                return BadRequest("Invalid request id");
            }

            var _manager = new Manager<Request>(_uow);

            var request = await _manager.GetByIdAsync(requestDto.Id, "RequestRisk,RequestAction".ToStringList());

            if (request == null)
            {
                return NotFound($"Request with ID {requestDto.Id} not found.");
            }


            if(requestDto.Report != null)
            {
                request.report = requestDto.Report;
            }



            if (request.Status == RequestStatus.InProgress && requestDto.Status != RequestStatus.underReview)
            {

                foreach (var actionDto in requestDto.Actions)
                {

                    request.RequestAction.Add(new RequestActionMapping
                    {

                        Action = (actionDto.Id == null) ? new Actions
                        {
                            ActionDescription = actionDto.Description!,
                            ActionType = (ActionType)actionDto.ActionType!,
                            Custom = true

                        } : null,
                        ActionID = (actionDto.Id == null) ? 0 : actionDto.Id.Value

                    });

                }

                var updatedRequest = await _manager.AddUpdateAsync(request);
                return Ok("request updated");
            }

            

            if (requestDto.Status == RequestStatus.closed)
            {
                var _managerLog = new Manager<Log>(_uow);
                

                Log newLog = new Log
                {
                    WorkEntity = request.WorkEntity,
                    Year = request.Year,
                    Occured = request.Occured,
                    Category = request.Category,
                    RiskName = request.Risk.RiskName,
                    Likelihood = request.Likelihood,
                    Impact = request.Impact,
                    Responsible = request.Responsible,
                    Description = request.Description,
                    report = request.report,
                    PostImpact = request.PostImpact,
                    PostLikelihood = request.PostLikelihood


                };

                newLog.LogActionsMappings = new List<LogActionsMapping>();
                foreach (var reqAction in request.RequestAction)
                {
                    newLog.LogActionsMappings.Add(new LogActionsMapping
                    {
                        ActionID = reqAction.ActionID
                    });
                }

                await _managerLog.AddUpdateAsync(newLog);
                await _manager.DeleteAsync(request);

                await _uow.SaveAsync();
                return Ok("nice");

            }

            // i want to break the loop of resending the request if it got rejected



            request.Status = requestDto.Status;
            var result = await _manager.AddUpdateAsync(request);
            return Ok("changed successfuly");


        }



    }
}