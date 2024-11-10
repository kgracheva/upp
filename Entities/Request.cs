using Entities;

namespace upp.Entities
{
    public class Request : BaseEntity
    {
        public int EntityId { get; set; }
        public string Comment { get; set; } = "";
        public int StatusTypeId { get; set; }
        public StatusType? StatusType { get; set; }
        public int OperatorId { get; set; }
        public User? Operator { get; set; }
        public RequestType RequestType { get; set; }
    }

    public enum RequestType
    {
        Article,
        Recipe,
        Training 
    }
}
