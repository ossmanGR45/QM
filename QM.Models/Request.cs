using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static QM.Models.Enums;


namespace QM.Models
{
    public class Request : EntityBase
    {

        public DateTime Year { get; set; }

        public bool Before { get; set; }

        
        public Likelihood Likelihood { get; set; }

        
        public Impact Impact { get; set; }

        
        public DateTime ExpectedTime { get; set; }

        [MaxLength(100)]
        public string Responsible { get; set; }

        public string Description { get; set; }

        
        public RequestStatus Status { get; set; }

        [MaxLength(500)]
        public string OutcomeSummary { get; set; }

        
        public Impact ImpactAfter { get; set; }

        
        public Likelihood LikelihoodAfter { get; set; }

        public bool Occured { get; set; }




        #region navigation Properties
        
        public ICollection<RequestActionMapping> RequestAction { get; set; }
        public ICollection<RequestCauseMapping> RequestCause { get; set; }
        public ICollection<RequestRiskMapping> RequestRisk { get; set; }






        #endregion
    }
}