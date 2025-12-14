using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM.Models
{
    public class WorkEntity : EntityBase
    {


        public string Name { get; set; }

        public ICollection<Actions> Actions { get; set; }
        
    }
}
