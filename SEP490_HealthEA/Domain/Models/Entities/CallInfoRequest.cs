namespace Domain.Models.Entities
{
    public class CallInfoRequest
    {
        public Guid CallerID { get; set; }
        public Guid CalleeUserId { get; set; }
    }
}
