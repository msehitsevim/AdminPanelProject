using Microsoft.AspNetCore.Identity;

namespace AdminPanel.Models;
public  class AppUser : IdentityUser
{
    public Guid? CompanyId { get; set; }
    public string? Token { get; set; }
}
