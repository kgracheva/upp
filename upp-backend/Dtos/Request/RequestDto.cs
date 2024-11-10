using upp.Entities;

namespace upp.Dtos.Request
{
    public class RequestDto
    {
        public int Id { get; set; }
        public int EntityId { get; set; }
        public RequestType RequestType { get; set; }
        public string Comment { get; set; } = "";
        public int StatusTypeId { get; set; }
        public int StatusTypeName { get; set; }
        public int OperatorId { get; set; }
        public int OperatorName { get; set; }
    }
}
