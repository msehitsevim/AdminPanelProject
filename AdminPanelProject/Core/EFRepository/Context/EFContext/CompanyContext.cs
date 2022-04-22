using AdminPanel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Core.Context.EFContext
{
    public class CompanyContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-HKVUQ28\\SQLEXPRESS; Database=AdminPanel; Integrated Security=true;");
        }
    }

}
