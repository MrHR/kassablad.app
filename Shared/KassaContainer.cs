using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kassablad.app;
public class KassaContainer 
{
    public int KassaContainerId { get; set; }
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
    public string? Activiteit { get; set; }
    [Required]
    public DateTime BeginUur { get; set; }
    public DateTime EindUur { get; set; }
    public string? Notes { get; set; }
    public int Bezoekers { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Afroomkluis { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    [Required]
    public decimal InkomstBar { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal InkomstLidkaart { get; set; }
    public bool Concept { get; set; }
    public virtual ICollection<Kassa>? Kassas { get; set; }
    public virtual ICollection<ConsumptieCount>? ConsumptieCounts { get; set; }
}