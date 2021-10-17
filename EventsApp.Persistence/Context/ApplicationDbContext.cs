using EventsApp.Domain;
using EventsApp.Domain.POCO;
using Microsoft.EntityFrameworkCore;

namespace EventsApp.Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {

        #region Ctor

        private readonly string _connectionString;
        public ApplicationDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        #endregion

        #region Configuration
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        #endregion

        #region DBSets

        public DbSet<Event> Events { get; set; }
        public DbSet<AppConfig> AppConfigs { get; set; }
        public DbSet<ApiUser> Users { get; set; }

        #endregion
    }
}
