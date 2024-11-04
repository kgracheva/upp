using Microsoft.AspNetCore.Identity;

namespace Entities
{
    public class AdditionalInfo : BaseEntity
    {
        public User? User { get; set; }
		//public string? Fio {  get; set; }
        public string Name { get; set; } = "";
        public string Surname { get; set; } = "";
        public string? Patronymic { get; set; }
    }

}