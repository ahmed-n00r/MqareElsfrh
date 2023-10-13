using AuthorizeLibrary.IdentityModel;
using DBModels.AppModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace AuthorizeLibrary.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {

        public static string DBConnctionString { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder) =>
            builder.UseSqlServer(DBConnctionString);

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>().ToTable("User");
            builder.Entity<IdentityRole>().ToTable("Role");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRole");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserToken");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim");

            builder.Entity<StudentTask>()
                .HasOne<IdentityRole>()
                .WithMany()
                .HasForeignKey(c => c.RoleId);

            builder.Entity<StudentDuty>()
                .HasOne<AppUser>()
                .WithMany()
                .HasForeignKey(c => c.UserId);
        }

        public DbSet<Group> Groups { get; set; }
        public DbSet<StudentTask> StudentTasks { get; set; }
        public DbSet<StudentDuty> StudentDuties { get; set; }
    }
}