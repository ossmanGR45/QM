using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static QM.Models.Enums;

namespace QM.Models
{
    public class logExtention : EntityBase
    {
        public List<string> ActionsAtEvent { get; set; }
        public Likelihood Likelihood { get; set; }
        public Impact Impact { get; set; }
    }
}
