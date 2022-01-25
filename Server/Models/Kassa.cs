using System.ComponentModel.DataAnnotations;
using kassablad.app.Shared;

namespace kassablad.app.Server.Models;
public class Kassa {
    public int KassaId { get; set; }
    [Required]
    public bool Active { get; set; }
    [Required]
    public DateTime DateAdded { get; set; }
    [Required]
    public DateTime DateUpdated { get; set; }
    [Required]
    public string? UpdatedBy { get; set; }
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