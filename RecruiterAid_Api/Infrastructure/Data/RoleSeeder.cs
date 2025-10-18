using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace RecruiterAid_Api.Infrastructure.Data.Seeders
{
    public static class RoleSeeder
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
        {
            foreach (var role in new[] { "ADMIN", "MANAGER", "AGENT" })
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
