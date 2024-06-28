using Microsoft.EntityFrameworkCore;
using RentHome.Shared.Models;

namespace RentHome.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {
            
        }

        public DbSet<House>  Houses { get; set; }
    }
}
