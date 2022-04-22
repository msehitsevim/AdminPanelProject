using AdminPanel.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace  Core.Context.EFContext
{
    public class IdentityContext : IdentityDbContext<AppUser,IdentityRole,string>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options):base(options)
        {

        }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
    }
}