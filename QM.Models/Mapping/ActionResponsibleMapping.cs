using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM.Models.Mapping
{
    public class ActionResponsibleMapping : EntityBase
    {
        public int ActionId { get; set; }
        public Actions Action { get; set; }
        public int ResponsibleId { get; set; }
        public Responsible Responsible { get; set; }
    }
}
