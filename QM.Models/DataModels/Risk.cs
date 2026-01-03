using QM.Models.Mapping;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static QM.Models.Enums;

namespace QM.Models.DataModels
{
    public class Risk : EntityBase
    {
    
        public string RiskName { get; set; }
        public string RiskDescription { get; set; }
        public string Location { get; set; }

        public Likelihood likelihood { get; set; }
        public Impact Impact { get; set; }

        public bool Custom { get; set; }



        // Foreign Key to Category
        public int? UserId { get; set; }
        public int CategoryID { get; set; }

        // Navigation Properties
        public User? User { get; set; }
        public Category Category { get; set; }
        public ICollection<RiskCauseMapping> RiskCauses { get; set; }
        public ICollection<RiskActionMapping> RiskActions { get; set; }
        public ICollection<RiskStrategicGoalMapping> RiskGoals { get; set; }
        public ICollection<Request> Requests { get; set; }

    }
}


