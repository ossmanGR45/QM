using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM.Models.Mapping
{
    public class RequestActionMapping : EntityBase
    {
        public int RequestID { get; set; }
        public Request Request { get; set; }
        public int ActionID { get; set; }
        public Actions Action { get; set; }
    }
}
