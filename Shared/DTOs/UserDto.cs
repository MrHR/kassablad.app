using System.ComponentModel.DataAnnotations;

namespace kassablad.app.Shared;

public class UserDto {
    public string? Id { get; set; }
    public string? Email { get; set; }
    public string? UserName { get; set; }
    // TODO: public List<RolesDto> RoleDtos { get; set; }
}