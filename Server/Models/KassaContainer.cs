using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kassablad.app.Server.Models;
public enum States {
    Activiteit,
    BeginKassa,
    EindKassa,
    Info,
    Consumpties
}
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
    public string? UpdatedBy { get; set; }
    [Required]
    public string? CreatedBy { get; set; }
    public string? Activiteit { get; set; }
    public DateTime BeginUur { get; set; }
    public DateTime EindUur { get; set; }
    public string? Notes { get; set; }
    public int Bezoekers { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Afroomkluis { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal InkomstBar { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal InkomstLidkaart { get; set; }
    [Required]
    public States State { get; set; }

    public virtual ICollection<Kassa>? Kassas { get; set; }
    public virtual ICollection<ConsumptieCount>? ConsumptieCounts { get; set; }

    // Many-to-may relationship with kassaContainerApplicationUsers table
    // More info: KassaContainerApplicationuser.cs
    public ICollection<ApplicationUser>? ApplicationUsers { get; set; }
    public List<KassaContainerApplicationUser>? KassaContainerApplicationUsers { get; set; }
    public int FKassaId { get; set; }
    public virtual FKassa? FKassa { get; set; }
}