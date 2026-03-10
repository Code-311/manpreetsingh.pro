using Microsoft.AspNetCore.Identity;

namespace manpreetsingh.pro.Models.Identity;

public class ApplicationUser : IdentityUser
{
    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
}
