using Microsoft.AspNetCore.Identity;
using upp.Entities;

namespace Entities
{
    public class User : IdentityUser<int> 
    {
        // public virtual ICollection<ApplicationUserRole> Roles { get; set; }
        public AdditionalInfo? Info { get; set; }
        public ICollection<UserRole> Roles { get; set; } = new List<UserRole>();
        public ICollection<Product> Products { get; set; } = new List<Product>();
        public ICollection<Calendar> Calendars { get; set; } = new List<Calendar>();
        public ICollection<Article> Articles { get; set; } = new List<Article>();
        public ICollection<Training> Trainings { get; set; } = new List<Training>();
        public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
        public ICollection<Request> Requests { get; set; } = new List<Request>();
    }

}