using QM.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM.Models.Mapping
{
    public class LogActionsMapping : EntityBase
    {
        public int logId { get; set; }
        public int ActionID { get; set; }

        #region single Navigation Property
        public Log Log { get; set; }
        public Actions Actions { get; set; }
        #endregion
    }
}
