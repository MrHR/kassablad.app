using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kassablad.app.Server.Models;
public class KassaNomination {
    public int KassaNominationId { get; set; }
    [Required]
    public bool Active { get; set; }
    [Required]
    public DateTime DateAdded { get; set; }
    [Required]
    public DateTime DateUpdated { get; set; }
    [Required]
    public int CreatedBy { get; set; }
    [Required]
    public int Amount { get; set; }
    [Required]
    public int KassaId { get; set; }
    [Required]
    public virtual Kassa? Kassa { get; set; }
    [Required]
    public int NominationId { get; set; }
    [Required]
    public virtual Nomination? Nomination { get; set; }
    [Required]
    public Nom? Nom { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Total { get; private set; } // private because this is a computed value (definition in dbcontext)
}