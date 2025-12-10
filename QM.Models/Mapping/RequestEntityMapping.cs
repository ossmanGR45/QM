namespace QM.Models.Mapping
{
    public class RequestEntityMapping : EntityBase
    {
        public int RequestID { get; set; }
        public Request Request { get; set; }
        public int EntityID { get; set; }
        public Entity Entity { get; set; }
    }
}