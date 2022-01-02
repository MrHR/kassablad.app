using System.ComponentModel.DataAnnotations;

namespace kassablad.app.Shared.Models;

public class Tapper {
    public int TapperId { get; set; }
    [Required]
    public bool Active { get; set; }
    [Required]
    public DateTime DateAdded { get; set; }
    [Required]
    public DateTime DateUpdated { get; set; }
    [Required]
    public int CreatedBy { get; set; }
    [Required]
    public int UpdatedBy { get; set; }
    [Required]
    public string? First_Name { get; set; }
    [Required]
    public string? Last_Name { get; set; }
}