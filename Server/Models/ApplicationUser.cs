using Microsoft.AspNetCore.Identity;

namespace kassablad.app.Server.Models;

public class ApplicationUser : IdentityUser
{
    public ICollection<KassaContainer>? KassaContainers { get; set; }
}
