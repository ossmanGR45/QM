using QM.Models.Mapping;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QM.Models
{
    public class Cause : EntityBase
    {
        
        
        public string CauseDescription { get; set; }

        // Navigation Properties
        public ICollection<RiskCauseMapping> RiskCauses { get; set; }
        public ICollection<ActionCauseMapping> ActionCauses { get; set; }
        public ICollection<RequestCauseMapping> RequestCauses { get; set; }

    }
}