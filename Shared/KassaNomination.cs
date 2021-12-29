using System.ComponentModel.DataAnnotations;

namespace kassablad.app;
public class KassaNomination {
    public int KassaNominationId { get; set; }
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
    public int KassaId { get; set; }
    [Required]
    public int NominationId { get; set; }
    [Required]
    public int Amount { get; set; }
    [Required]
    public virtual Kassa? Kassa { get; set; }
    [Required]
    public virtual Nomination? Nomination { get; set; }
    
}