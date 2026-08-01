using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KASHOP.PL.Utilities
{
    public class RoleSeedData : ISeedData
    {
        private readonly RoleManager<IdentityRole> _rolemanager;

        public RoleSeedData(RoleManager<IdentityRole> roleManager)
        {
            _rolemanager = roleManager;
        }
        public async Task DataSeed()
        {
            string[] roles = { "SuperAdmin", "Admin", "User" };
            if(!await _rolemanager.Roles.AnyAsync())
            {
                foreach (var role in roles)
                {
                    await _rolemanager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
