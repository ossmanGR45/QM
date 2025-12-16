using QM.Models.Mapping;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QM.Models
{
    public class Responsible : EntityBase
    {
        

        public string EntityName { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhoneNumber { get; set; }
        public ICollection<ActionResponsibleMapping> ActionResponsibles { get; set; }

    }
}