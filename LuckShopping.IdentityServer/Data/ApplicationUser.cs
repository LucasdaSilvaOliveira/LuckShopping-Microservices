using Microsoft.AspNetCore.Identity;

namespace LuckShopping.IdentityServer.Data
{
    public class ApplicationUser : IdentityUser
    {
        private string FirstName { get; set; }

        private string LastName { get; set; }
    }
}
