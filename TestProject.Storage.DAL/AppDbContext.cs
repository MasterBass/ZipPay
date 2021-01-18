using Microsoft.EntityFrameworkCore;
using TestProject.Core.Domain;

namespace TestProject.Storage.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        
        #region DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(x => x.Email).IsUnique();
        }
    }
}