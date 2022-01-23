using System.ComponentModel.DataAnnotations;

namespace kassablad.app.Server.Models;
public enum Statuses {
    Open,
    Counted,
    Closed
}
public class Kassa {
    public int KassaId { get; set; }
    [Required]
    public bool Active { get; set; }
    [Required]
    public DateTime DateAdded { get; set; }
    [Required]
    public DateTime DateUpdated { get; set; }
    [Required]
    public int UpdatedBy { get; set; }
    [Required]
    public string? CreatedBy { get; set; }
    [Required]
    public int KassaContainerId { get; set; }
    [Required]
    public string? Type { get; set; }
    [Required]
    public Statuses Status { get; set; }
    public virtual KassaContainer? KassaContainer { get; set; }
    public virtual ICollection<KassaNomination>? KassaNominations { get; set; }
}