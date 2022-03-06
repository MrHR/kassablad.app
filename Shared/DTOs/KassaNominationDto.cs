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
public class KassaNominationDto {
    public int KassaNominationId { get; set; }
    public int KassaId { get; set; }
    public int Amount { get; set; }
    public decimal Multiplier { get; set; }
    public decimal Total { get; set; }
    public int NominationId { get; set; }
    public NominationDto? Nomination { get; set; }
}