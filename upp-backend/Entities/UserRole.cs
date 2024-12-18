using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class UserRole : IdentityUserRole<int>
    {
        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}