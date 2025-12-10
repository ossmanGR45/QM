using System.ComponentModel.DataAnnotations;

namespace QM.Models.Mapping
{
    public class RiskGoalMapping : EntityBase
    {
        
        public int RiskID { get; set; }

        public int GoalID { get; set; }

        #region single Navigation Property
        public Risk Risk { get; set; }

        public StrategicGoal StrategicGoal { get; set; }

        #endregion
    }
}