using QM.Models.Mapping;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static QM.Models.Enums;

namespace QM.Models.DataModels
{
    public class Actions : EntityBase
    {

        public string? ActionDescription  { get; set; }
        public ActionType? ActionType { get; set; }
        public bool? Custom { get; set; }


        // Navigation Properties
        public  ICollection<RiskActionMapping> RiskActions { get; set; }
        public  ICollection<ActionCauseMapping> ActionCauses { get; set; }
        public  ICollection<RequestActionMapping> RequestActions { get; set; }
        public  ICollection<ActionResponsibleMapping> ActionResponsibles { get; set; }
        public  ICollection<LogActionsMapping> LogActionsMappings { get; set; }
    }
}