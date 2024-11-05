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
    }

}