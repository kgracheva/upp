using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Entities
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int, ApplicationUserClaim, UserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>
    {


        public ApplicationDbContext(
         DbContextOptions<ApplicationDbContext> options)
         : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AdditionalInfo>()
                .HasOne(x => x.User)
                .WithOne(x => x.Info)
                .HasForeignKey<AdditionalInfo>(x => x.Id);


            builder.Entity<UserRole>()
                .HasOne(x => x.Role)
                .WithMany()
                .HasForeignKey(x => x.RoleId);

            builder.Entity<UserRole>()
                .HasOne(x => x.User)
                .WithMany(x => x.Roles)
                .HasForeignKey(x => x.UserId);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var modifiedEntries = ChangeTracker
                    .Entries()
                    .Where(x => (x.State == EntityState.Modified || x.State == EntityState.Added) && typeof(BaseEntity).IsAssignableFrom(x.Entity.GetType()))
                    .Select(x => x.Entity)
                    .ToList();

            if (modifiedEntries.Count() > 0)
            {
                modifiedEntries.ForEach(x =>
                {
                    var e = x as BaseEntity;

                    if (e.Created == null)
                        e.Created = DateTime.UtcNow;

                    e.LastModified = DateTime.UtcNow;
                });
            }


            return base.SaveChangesAsync(cancellationToken);
        }
    }
}