using Microsoft.AspNetCore.Identity;

namespace Entities
{
    public class User : IdentityUser<int> 
    {
        // public virtual ICollection<ApplicationUserRole> Roles { get; set; }
        public AdditionalInfo? Info { get; set; }
    }

}