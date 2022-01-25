/**
* This is a DTO, basically a data transfer object used to map data to be sent to client and vice versa. 
* You should use this if the model containes to much data, or if you want to exclude data that the client shouldn't see.
* I am choosing not to use this right now because it means a lot of extra work and I don't need the security or 
* speed for now.
*
* Documentation: https://docs.microsoft.com/en-us/aspnet/web-api/overview/data/using-web-api-with-entity-framework/part-5
*
*/


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kassablad.app.Shared;

// Model to use when posting/putting to controller
public class NominationDto {    
    public int NominationId { get; set; }
    public bool Active { get; set; }
    public DateTime DateAdded { get; set; }
    public DateTime DateUpdated { get; set; }
    public int UpdatedBy { get; set; }
    public string CreatedBy { get; set; } = String.Empty;
    public int DefaultAmount { get; set; }
    public string Text { get; set; } = String.Empty;
    public NomDto Nom { get; set; } = new NomDto();
}

public class NomDto {
    [Column(TypeName = "decimal(18,2)")]
    public decimal Multiplier { get; set; }
    public string? Currency { get; set; } = "€";
}