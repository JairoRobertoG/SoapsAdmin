using Microsoft.AspNetCore.Identity;

namespace Soaps.Model
{
    public class ApplicationUser : IdentityUser
    {
        public UserRegister UserRegister { get; set; }
    }
}
