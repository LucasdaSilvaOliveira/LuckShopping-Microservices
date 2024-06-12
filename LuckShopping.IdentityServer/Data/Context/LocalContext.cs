using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LuckShopping.IdentityServer.Data.Context
{
    public class LocalContext : IdentityDbContext<ApplicationUser>
    {
        public LocalContext(DbContextOptions<LocalContext> opt) : base(opt)
        {
            
        }
    }
}
