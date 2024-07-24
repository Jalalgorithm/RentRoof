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
        public DbSet<Mode> Modes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TypeOfProperty> PropertyTypes { get; set; }
        public DbSet<HouseImage> HouseImages { get; set; }
        public DbSet<AgentStatus> AgentStatuses { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Agent> Agents { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<House>()
                .Property(p => p.Price)
                .HasColumnType("decimal(16,2)");


            modelBuilder.Entity<Appointment>()
                .HasOne(ap => ap.House)
                .WithMany(p => p.Appointments)
                .HasForeignKey(ap => ap.HouseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
                .HasOne(ap => ap.Agent)
                .WithMany(a => a.Appointments)
                .HasForeignKey(ap => ap.AgentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
                .HasOne(ap => ap.User)
                .WithMany(u => u.Appointments)
                .HasForeignKey(ap => ap.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
