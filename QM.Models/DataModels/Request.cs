using QM.Models.Mapping;
using static QM.Models.Enums;


namespace QM.Models.DataModels
{
    public class Request : EntityBase
    {
        public string? WorkEntity { get; set; } = null;
        public DateTime? Year { get; set; } = null;
        public string? Category { get; set; } = null; 
        public Likelihood? Likelihood { get; set; } = null;
        public Impact? Impact { get; set; } = null;
        public DateTime? ExpectedTime { get; set; } = null;
        public string? Responsible { get; set; } = null;
        public string? Description { get; set; } = null;
        public RequestStatus? Status { get; set; } = null;
        public bool? Occured { get; set; } = null;
        public Likelihood? PostLikelihood { get; set; } = null;
        public Impact? PostImpact { get; set; } = null;
        public string? report { get; set; } = null;
        public int? UserId { get; set; } = null;
        public User? User { get; set; } = null;
        public int? RiskId { get; set; } = null;
        public Risk? Risk { get; set; } = null;
        public ICollection<RequestActionMapping>? RequestAction { get; set; } = null;

    }
}