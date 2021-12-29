using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kassablad.app.Server.Models;

public class Nom {
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Multiplier { get; set; }
    [Required]
    public string? Currency { get; set; } = "€";
}