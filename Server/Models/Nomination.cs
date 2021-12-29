using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;

namespace kassablad.app.Server.Models;
public class Nomination {
    public int NominationId { get; set; }
    [Required]
    public bool Active { get; set; }
    [Required]
    public DateTime DateAdded { get; set; }
    [Required]
    public DateTime DateUpdated { get; set; }
    [Required]
    public int UpdatedBy { get; set; }
    [Required]
    public int CreatedBy { get; set; }
    [Required]
    public int DefaultAmount { get; set; }
    public string Total { get; set; } = String.Empty;
    public virtual ICollection<KassaNomination>? KassaNominations { get; set; }
    [Required]
    public Nom? Nom { get; set; } // The Ownership of this keyless value object is defined in dbContext with fluent API
}
public class Nom {
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Multiplier { get; set; }
    [Required]
    public string? Currency { get; set; } = "€";
}