using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class RepositoryContext : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options) {}
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
           /* // PostgreSQL uses the public schema by default - not dbo.
            builder.HasDefaultSchema("public");
            base.OnModelCreating(builder);

            //Rename Identity tables to lowercase
            foreach (var entity in builder.Model.GetEntityTypes())
            {
                var currentTableName = builder.Entity(entity.Name).Metadata.GetDefaultTableName();
                builder.Entity(entity.Name).ToTable(currentTableName.ToLower());
            }

            base.OnModelCreating(builder);

            var admin_id = Guid.NewGuid();
            var role_admin_id = Guid.NewGuid();

            builder.Entity<IdentityRole<Guid>>().HasData(new IdentityRole<Guid>
            {
                Id = role_admin_id,
                Name = "admin",
                NormalizedName = "admin",
            });

            var hasher = new PasswordHasher<IdentityUser<Guid>>();
            builder.Entity<IdentityUser<Guid>>().HasData(new IdentityUser<Guid>
            {
                Id = admin_id,
                UserName = "admin",
                NormalizedUserName = "admin",
                PasswordHash = hasher.HashPassword(null, "admin"),
                Email = "admin@pmsoft.ru",
                NormalizedEmail = "admin@pmsoft.ru",
                EmailConfirmed = true,
                SecurityStamp = String.Empty
            });

            builder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = role_admin_id,
                UserId = admin_id
            });*/
        }
        DbSet<Book> Books { get; set; }
    }
}
