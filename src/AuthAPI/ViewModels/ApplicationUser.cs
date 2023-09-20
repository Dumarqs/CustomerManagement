using Microsoft.AspNetCore.Identity;

namespace AuthAPI.ViewModels
{
    public class ApplicationUser : IdentityUser
    {
        public string Role { get; set; }
    }
}
