using System.ComponentModel.DataAnnotations;

namespace kassablad.app;
public class Consumptie {
    public int ConsumptieId { get; set; }
    public bool Active { get; set; }
    [Timestamp]
    public DateTime DateAdded { get; set; }
    public string? Type { get; set; }
    [Timestamp]
    public DateTime DateUpdated { get; set; }
    public int CreatedBy { get; set; }
    public int UpdatedBy { get; set; }
    public string? Naam { get; set; }
    public decimal Prijs { get; set; }
    public virtual ICollection<ConsumptieCount>? ConsumptieCounts { get; set; }
}