using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static QM.Models.Enums;

namespace QM.Models.DTO
{
    public class RequestDto : EntityBase
    {
        public string? Department { get; set; }
        public DateTime? Year { get; set; }
        public string? Category { get; set; }
        public Likelihood? Likelihood { get; set; }
        public Impact? Impact { get; set; }
        public Likelihood? PostLikelihood { get; set; } = null;
        public Impact? PostImpact { get; set; } = null;
        public DateTime? ExpectedTime { get; set; } = null;
        public string? Description { get; set; } = null;
        public RequestStatus? Status { get; set; } = null;
        public bool? Occured { get; set; } = null;
        public string? rejectReason { get; set; } = null;
        public bool? ReDirected { get; set; } = null;
        public int? ResponsibleId { get; set; } = null;
        public int? UserId { get; set; } = null;
        public int? RiskId { get; set; } = null;

        



        public List<ActionInputDto>? Actions { get; set; } = new List<ActionInputDto>();




    }
}
