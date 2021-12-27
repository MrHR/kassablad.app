using System.ComponentModel.DataAnnotations;

namespace kassablad.app;
public class ConsumptieCount {
    public int ConsumptieCountId { get; set; }
    [Required]
    public bool Active { get; set; }
    [Timestamp]
    public DateTime DateAdded { get; set; }
    [Timestamp]
    public DateTime DateUpdated { get; set; }
    [Required]
    public int CreatedBy { get; set; }
    [Required]
    public int UpdatedBy { get; set; }
    [Required]
    public int KassaContainerId { get; set; }
    [Required]
    public int ConsumptieId { get; set; }
    [Required]
    public int Aantal { get; set; }
    public virtual KassaContainer? KassaContainer { get; set; }
    public virtual Consumptie? Consumptie { get; set; }
}