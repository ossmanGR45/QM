using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static QM.Models.Enums;

namespace QM.Models.DTO
{
    public class RequestDto : EntityBase
    {
        public string WorkEntity { get; set; }
        public DateTime Year { get; set; }
        public string Category { get; set; }

        // Using the Enums directly is fine as long as they are sent as Ints or Strings from JSON
        public Likelihood Likelihood { get; set; }
        public Impact Impact { get; set; }

        public DateTime ExpectedTime { get; set; }
        public string Responsible { get; set; }
        public string Description { get; set; }
        public RequestStatus Status { get; set; }
        public bool Occured { get; set; }
        public string? Report { get; set; }


        // This is where the React app sends the list of mixed actions
        public List<ActionInputDto> Actions { get; set; } = new List<ActionInputDto>();

        // You can follow the same pattern for Risks if needed
        public int RiskId { get; set; }
    }
}
