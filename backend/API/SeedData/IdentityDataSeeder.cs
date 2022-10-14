using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace API.SeedData
{
    public class IdentityDataSeeder
    {
        private readonly UserManager<IdentityUser<Guid>> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        public IdentityDataSeeder(UserManager<IdentityUser<Guid>> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            _userManager = userManager;   
            _roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            var admin_id = Guid.Parse("64103b43-c6ba-4997-8849-a5ef75611390");
            var role_admin_id = Guid.Parse("d67ca879-2581-4f30-967c-3178d5697931");
            var adminRole = new IdentityRole<Guid>
            {
                Id = role_admin_id,
                Name = "admin",
                NormalizedName = "admin",
            };
            var adminPassword = "admin";

            await CreateRoleAsync(adminRole);


            var adminUser = new IdentityUser<Guid>
            {
                Id = admin_id,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "admin@pmsoft.ru",
                NormalizedEmail = "admin@pmsoft.ru",
                EmailConfirmed = true,
                SecurityStamp = String.Empty
            };

            await CreateUserAsync(adminUser, adminPassword);
            var adminIsInRole = await _userManager.IsInRoleAsync(adminUser, adminRole.Name);
            if (!adminIsInRole)
                await _userManager.AddToRoleAsync(adminUser, adminRole.Name);
        }

        private async Task CreateRoleAsync(IdentityRole<Guid> role)
        {
            var isExists = await _roleManager.RoleExistsAsync(role.Name);
            if (!isExists)
                await _roleManager.CreateAsync(role);
        }

        private async Task CreateUserAsync(IdentityUser<Guid> user, string password)
        {
            var isExists = await _userManager.FindByNameAsync(user.UserName);
            if (isExists == null)
                await _userManager.CreateAsync(user, password.ToString());
        }
    }
}
