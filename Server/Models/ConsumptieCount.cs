using System.ComponentModel.DataAnnotations;

namespace kassablad.app.Server.Models;
public class ConsumptieCount {
    public int ConsumptieCountId { get; set; }
    [Required]
    public bool Active { get; set; }
    [Required]
    public DateTime DateAdded { get; set; }
    [Required]
    public DateTime DateUpdated { get; set; }
    [Required]
    public string? CreatedBy { get; set; }
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