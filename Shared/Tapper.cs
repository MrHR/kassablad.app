using System.ComponentModel.DataAnnotations;

namespace kassablad.app;

public class Tapper {
    public int TapperId { get; set; }
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
    public string? First_Name { get; set; }
    [Required]
    public string? Last_Name { get; set; }
}