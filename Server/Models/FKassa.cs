using System.ComponentModel.DataAnnotations;

namespace kassablad.app.Server.Models;
public class FKassa {
    public int FKassaId { get; set; }
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
    public string? FKassaNaam { get; set; }
    public List<KassaContainer>? KassaContainers { get; set; }
}