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

namespace kassablad.app.Shared;

// Model to use when posting/putting to controller
public class FKassaDto {
    public int FKassaId { get; set; }
    [Required]
    public string? FKassaNaam { get; set; }

    public KassaContainerReturnDto KassaContainerDto { get; set; } = new KassaContainerReturnDto();
}