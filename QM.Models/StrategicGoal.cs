using QM.Models.Mapping;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QM.Models
{
    public class StrategicGoal : EntityBase
    {
        

        public string GoalReference { get; set; }
        public string GoalDescription { get; set; }

        // Navigation Property
        public ICollection<RiskGoalMapping> RiskGoals { get; set; }
    }
}