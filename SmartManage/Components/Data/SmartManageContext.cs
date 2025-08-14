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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("SmartManageDatabase");
                optionsBuilder.UseSqlServer(connectionString);

            }
        }
    }
}
    