using Microsoft.EntityFrameworkCore;
using SmartManage.Components.Models;

namespace SmartManage.Components.Data
{
    public class SmartManageContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public SmartManageContext(DbContextOptions<SmartManageContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Record> Records { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("SmartManageDatabase");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Unique email constraint
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Owner relationship: a Record has optional Owner (User)
            modelBuilder.Entity<Record>()
                .HasOne(r => r.Owner)
                .WithMany()
                .HasForeignKey(r => r.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}