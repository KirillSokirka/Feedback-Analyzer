namespace Identity.Models;

public class ApplicationUser : Microsoft.AspNetCore.Identity.IdentityUser
{
    public string FullName { get; set; } = null!;
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
}