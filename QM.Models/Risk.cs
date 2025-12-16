using QM.Models.Mapping;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static QM.Models.Enums;

namespace QM.Models
{
    public class Risk : EntityBase
    {
    
        public string RiskName { get; set; }
        public string RiskDescription { get; set; }
        public string Location { get; set; }

        public Likelihood likelihood { get; set; }
        public Impact Impact { get; set; }

        // Foreign Key to Category
        public int CategoryID { get; set; }

        // Navigation Properties
        public Category Category { get; set; }
        public ICollection<RiskCauseMapping> RiskCauses { get; set; }
        public ICollection<RiskActionMapping> RiskActions { get; set; }
        public ICollection<RiskStrategicGoalMapping> RiskGoals { get; set; }
        public ICollection<RequestRiskMapping> RequestRisks { get; set; }

    }
}