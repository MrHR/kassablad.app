using System.ComponentModel.DataAnnotations;

namespace kassablad.app.Server.Models;
public class KassaTemplate {
    public int KassaTemplateId { get; set; }
    public bool Active { get; set; }
    [Required]
    public DateTime DateAdded { get; set; }
    [Required]
    public DateTime DateUpdated { get; set; }
    public int UpdatedBy { get; set; }
    public string? CreatedBy { get; set; }
    public string? Nominations { get; set; }
    
}