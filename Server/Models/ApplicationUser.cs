using Microsoft.AspNetCore.Identity;

namespace kassablad.app.Server.Models;

public class ApplicationUser : IdentityUser
{
    public ICollection<KassaContainer>? KassaContainers { get; set; } // Check KassaContainerTapper.cs
    public List<KassaContainerApplicationUser>? KassaContainerApplicationUsers { get; set; }
}
