using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kassablad.app;
public class Consumptie {
    public int ConsumptieId { get; set; }
    public bool Active { get; set; }
    [Required]
    public DateTime DateAdded { get; set; }
    public string? Type { get; set; }
    [Required]
    public DateTime DateUpdated { get; set; }
    public int CreatedBy { get; set; }
    public int UpdatedBy { get; set; }
    public string? Naam { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Prijs { get; set; }
    public virtual ICollection<ConsumptieCount>? ConsumptieCounts { get; set; }
}