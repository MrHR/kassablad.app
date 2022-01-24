/**
* This is a DTO, basically a data transfer object used to map data to be sent to client and vice versa. 
* You should use this if the model containes to much data, or if you want to exclude data that the client shouldn't see.
*
* Documentation: https://docs.microsoft.com/en-us/aspnet/web-api/overview/data/using-web-api-with-entity-framework/part-5
*
*/


using System.ComponentModel.DataAnnotations;

namespace kassablad.app.Shared;

// Model to use when posting/putting to controller
public class KassaDto {
    public int KassaId { get; set; }
    public bool Active { get; set; }
    public DateTime DateAdded { get; set; }
    public DateTime DateUpdated { get; set; }
    public int UpdatedBy { get; set; }
    public string? CreatedBy { get; set; }
    public int KassaContainerId { get; set; }
    public string? Type { get; set; }
}