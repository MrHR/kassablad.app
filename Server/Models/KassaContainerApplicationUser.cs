using System.ComponentModel.DataAnnotations;

namespace kassablad.app.Server.Models;

public class KassaContainerApplicationUser
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    [Required]
    public string? ApplicationUserId { get; set; }
    [Required]
    public ApplicationUser? ApplicationUser { get; set; }
    [Required]
    public int KassaContainerId { get; set; }
    [Required]
    public KassaContainer? KassaContainer { get; set; }
}