using System.ComponentModel.DataAnnotations;

namespace kassablad.app;
public class Nomination {
    public int NominationId { get; set; }
    [Required]
    public bool Active { get; set; }
    [Timestamp]
    public DateTime DateAdded { get; set; }
    [Timestamp]
    public DateTime DateUpdated { get; set; }
    [Required]
    public int UpdatedBy { get; set; }
    [Required]
    public int CreatedBy { get; set; }
    [Required]
    public string? Nom { get; set; }
    [Required]
    public decimal Multiplier { get; set; }
    [Required]
    public int DefaultAmount { get; set; }
    public string? Total { get; set; }
    public virtual ICollection<KassaNomination>? KassaNominations { get; set; }
}