using QM.Models.DataModels;

namespace QM.Models.Mapping
{
    public class RequestResponsibleMapping : EntityBase
    {
        public int RequestID { get; set; }
        public Request Request { get; set; }
        public int ResponsibleID { get; set; }
        public Responsible Responsible { get; set; }
    }
}