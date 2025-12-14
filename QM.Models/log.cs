using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static QM.Models.Enums;

namespace QM.Models
{
    public class log : EntityBase
    {
        public DateTime Year { get; set; }
        public bool Occured { get; set; }
        public string Category { get; set; }

        public string RiskName { get; set; }

        public Likelihood Likelihood { get; set; }

        public Impact Impact { get; set; }

        public List<string> AvoidenceActions { get; set; }
        
        public string Responsible { get; set; }

        public string Description { get; set; }

        public string? report { get; set; }

        public int LogExtentionId { get; set; }

        #region navigation Properties
        public logExtention LogExtention { get; set; }

        #endregion
    }
}
