using Microsoft.AspNetCore.Identity;

namespace LeagueApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Address { get; set; } = string.Empty;
    }
}
