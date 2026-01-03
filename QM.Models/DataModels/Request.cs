using QM.Models.Mapping;
using static QM.Models.Enums;


namespace QM.Models.DataModels
{
    public class Request : EntityBase
    {
        public string WorkEntity { get; set; }
        public DateTime Year { get; set; }

        public string Category { get; set; }


        public Likelihood Likelihood { get; set; }

        
        public Impact Impact { get; set; }

        
        public DateTime ExpectedTime { get; set; }

        
        public string Responsible { get; set; }

        public string Description { get; set; }

        
        public RequestStatus Status { get; set; }

        public bool Occured { get; set; }

        public Likelihood? PostLikelihood { get; set; }
        public Impact? PostImpact { get; set; }

        public string? report { get; set; }



        public int UserId { get; set; }
        public User User { get; set; }

        public int RiskId { get; set; } 
        public Risk Risk { get; set; }

        #region navigation Properties

        public ICollection<RequestActionMapping> RequestAction { get; set; }



        #endregion
    }
}