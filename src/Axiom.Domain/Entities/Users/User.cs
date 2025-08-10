using Axiom.Domain.Common;

namespace Axiom.Domain.Entities.Users;

public class User : Entity
{
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;

    public bool IsActive { get; set; }
    public bool IsAdmin { get; set; }
}