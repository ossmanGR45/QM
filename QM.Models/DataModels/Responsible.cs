using QM.Models.Mapping;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QM.Models.DataModels
{
    public class Responsible : EntityBase
    {
        public string? EntityName { get; set; } = null;
        public string? ContactName { get; set; } = null;
        public string? ContactEmail { get; set; } = null;
        public string? ContactPhoneNumber { get; set; } = null;

        // Navigation Properties
        public ICollection<ActionResponsibleMapping>? ActionResponsibles { get; set; } = null;
    }
}