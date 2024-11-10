using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using upp.Entities;
using System.Reflection.Emit;

namespace Entities
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int, ApplicationUserClaim, UserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>
    {

        public DbSet<AdditionalInfo> AdditionalInfo { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Calendar> Calendars { get; set; }
        public DbSet<MealType> MealTypes { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<StatusType> StatusTypes { get; set; }
        public DbSet<ArticleBlock> ArticleBlocks { get; set; }
        public DbSet<TrainingBlock> TrainingBlocks { get; set; }
        public DbSet<RecipeBlock> RecipeBlocks { get; set; }
        public DbSet<Block> Blocks { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Request> Requests { get; set; }
        public ApplicationDbContext(
         DbContextOptions<ApplicationDbContext> options)
         : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Request>()
                .HasOne(x => x.StatusType)
                .WithMany()
                .HasForeignKey(x => x.StatusTypeId);

            builder.Entity<Request>()
                .HasOne(x => x.Operator)
                .WithMany(x => x.Requests)
                .HasForeignKey(x => x.OperatorId);


            builder.Entity<RecipeBlock>()
                .HasKey(x => new { x.RecipeId, x.BlockId });
            builder.Entity<RecipeBlock>()
                .HasOne(x => x.Recipe)
                .WithMany(x => x.RecipeBlocks)
                .HasForeignKey(x => x.RecipeId);

            builder.Entity<RecipeBlock>()
                .HasOne(x => x.Block)
                .WithMany()
                .HasForeignKey(x => x.BlockId);

            builder.Entity<Recipe>()
                .HasOne(x => x.Creator)
                .WithMany(x => x.Recipes)
                .HasForeignKey(x => x.CreatorId);

            builder.Entity<Recipe>()
                .HasOne(x => x.StatusType)
                .WithMany()
                .HasForeignKey(x => x.StatusTypeId);

            builder.Entity<TrainingBlock>()
              .HasKey(x => new { x.TrainingId, x.BlockId });
            builder.Entity<TrainingBlock>()
                .HasOne(x => x.Training)
                .WithMany(x => x.TrainingBlocks)
                .HasForeignKey(x => x.TrainingId);

            builder.Entity<TrainingBlock>()
                .HasOne(x => x.Block)
                .WithMany()
                .HasForeignKey(x => x.BlockId);

            builder.Entity<Training>()
                .HasOne(x => x.Creator)
                .WithMany(x => x.Trainings)
                .HasForeignKey(x => x.CreatorId);

            builder.Entity<Training>()
                .HasOne(x => x.StatusType)
                .WithMany()
                .HasForeignKey(x => x.StatusTypeId);

            builder.Entity<ArticleBlock>()
                .HasKey(x => new { x.ArticleId, x.BlockId });
            builder.Entity<ArticleBlock>()
                .HasOne(x => x.Article)
                .WithMany(x => x.ArticleBlocks)
                .HasForeignKey(x => x.ArticleId);

            builder.Entity<ArticleBlock>()
                .HasOne(x => x.Block)
                .WithMany()
                .HasForeignKey(x => x.BlockId);

            builder.Entity<Article>()
                .HasOne(x => x.Creator)
                .WithMany(x => x.Articles)
                .HasForeignKey(x => x.CreatorId);

            builder.Entity<Article>()
                .HasOne(x => x.StatusType)
                .WithMany()
                .HasForeignKey(x => x.StatusTypeId);

            builder.Entity<Calendar>()
                .HasOne(x => x.User)
                .WithMany(x => x.Calendars)
                .HasForeignKey(x => x.UserId);

            builder.Entity<Calendar>()
                .HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId);

            builder.Entity<Calendar>()
                .HasOne(x => x.MealType)
                .WithMany()
                .HasForeignKey(x => x.MealTypeId);

            builder.Entity<Product>()
                .HasOne(x => x.Creator)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.CreatorId);

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