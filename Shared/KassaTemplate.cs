using System.ComponentModel.DataAnnotations;

namespace kassablad.app;
public class KassaTemplate {
    public int KassaTemplateId { get; set; }
    public bool Active { get; set; }
    [Timestamp]
    public DateTime DateAdded { get; set; }
    [Timestamp]
    public DateTime DateUpdated { get; set; }
    public int UpdatedBy { get; set; }
    public int CreatedBy { get; set; }
    public string? Nominations { get; set; }
    
}