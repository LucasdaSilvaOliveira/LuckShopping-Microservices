using LuckShopping.IdentityServer.Configuration;
using LuckShopping.IdentityServer.Data;
using LuckShopping.IdentityServer.Data.Context;
using Microsoft.AspNetCore.Identity;

namespace LuckShopping.IdentityServer.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly LocalContext _localContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationUser> _roleManager;

        public DbInitializer(LocalContext localContext, UserManager<ApplicationUser> userManager, RoleManager<ApplicationUser> roleManager)
        {
            _localContext = localContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public void Initialize()
        {
            if (_roleManager.FindByNameAsync(IdentityConfiguration.Admin).Result != null) return;

            _roleManager.CreateAsync(new IdentityRole(IdentityConfiguration.Admin)).GetAwaiter().GetResult();

            _roleManager.CreateAsync(new IdentityRole(IdentityConfiguration.Client)).GetAwaiter().GetResult();
        }
    }
}
