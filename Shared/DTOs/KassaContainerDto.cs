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

public class KassaContainerDto {
    public int KassaContainerId { get; set; }
    [Required]
    public bool Active { get; set; }
    [Required]
    public DateTime DateAdded { get; set; }
    [Required]
    public DateTime DateUpdated { get; set; }
    [Required]
    public int UpdatedBy { get; set; }
    [Required]
    public string? CreatedBy { get; set; }
    [Required]
    public string? Activiteit { get; set; }
    public DateTime BeginUur { get; set; }
    public DateTime EindUur { get; set; }
    public string? Notes { get; set; }
    public int Bezoekers { get; set; }
    public decimal Afroomkluis { get; set; }
    public decimal InkomstBar { get; set; }
    public decimal InkomstLidkaart { get; set; }
    public bool Concept { get; set; }
}