using QM.Models.Mapping;
using static QM.Models.Enums;

namespace QM.Models.DataModels
{
    public class Log : EntityBase
    {

        public string WorkEntity { get; set; }
        public DateTime Year { get; set; }
        public bool Occured { get; set; }
        public string Category { get; set; }

        public string RiskName { get; set; }

        public Likelihood Likelihood { get; set; }

        public Impact Impact { get; set; }
        
        public string Responsible { get; set; }

        public string Description { get; set; }

        public string? report { get; set; }

        
        public Likelihood? PostLikelihood { get; set; }
        public Impact? PostImpact { get; set; }


        public ICollection<LogActionsMapping> LogActionsMappings { get; set; }


    }
}
