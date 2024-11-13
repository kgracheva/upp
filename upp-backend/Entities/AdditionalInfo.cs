using Microsoft.AspNetCore.Identity;

namespace Entities
{
    public class AdditionalInfo : BaseEntity
    {
        public User? User { get; set; }
		//public string? Fio {  get; set; }
        public string Name { get; set; } = "";
        public string Lastname { get; set; } = "";
        public int CaloriesCountByDay { get; set; }
    }

}