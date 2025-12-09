using System.ComponentModel.DataAnnotations;

namespace QM.Models
{
    public class RiskActionMapping : EntityBase
    {
        
        public int RiskID { get; set; }

        public int ActionID { get; set; }

        #region single Navigation Property
        public Risk Risk { get; set; }

        public Actions Action { get; set; }


        #endregion
    }
}