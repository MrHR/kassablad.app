namespace kassablad.app.Server.Models;

public class KassaContainerUser {
    public string? ApplicationUserId { get; set; }
    public ApplicationUser? Tapper { get; set; }
    public int KassaContainerId { get; set; }
    public KassaContainer? KassaContainer { get; set; }
}