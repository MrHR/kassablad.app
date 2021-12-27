using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.Schema;

namespace kassablad.app;
public class KassaContainer 
{
    public int KassaContainerId { get; set; }
    [Required]
    public bool Active { get; set; }
    [Timestamp]
    public DateTime DateAdded { get; set; }
    [Timestamp]
    public DateTime DateUpdated { get; set; }
    [Required]
    // [ForeignKey("TapperId")]
    public int UpdatedBy { get; set; }
    [Required]
    // [ForeignKey("TapperId")]
    public int CreatedBy { get; set; }
    [Required]
    public string? NaamTapper { get; set; }
    [Required]
    public string? Activiteit { get; set; }
    [Required]
    public DateTime BeginUur { get; set; }
    public DateTime EindUur { get; set; }
    public string? Notes { get; set; }
    [Required]
    public string? NaamTapperSluit { get; set; }
    public int Bezoekers { get; set; }
    public decimal Afroomkluis { get; set; }
    [Required]
    public decimal InkomstBar { get; set; }
    public decimal InkomstLidkaart { get; set; }
    public bool Concept { get; set; }
    public string? FormSection { get; set; }
    public virtual ICollection<Kassa>? Kassas { get; set; }
    public virtual ICollection<ConsumptieCount>? ConsumptieCounts { get; set; }
}