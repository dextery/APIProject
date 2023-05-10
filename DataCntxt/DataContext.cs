using Microsoft.EntityFrameworkCore;
using WEBAPIForUserAuthorization.Models;

namespace WEBAPIForUserAuthorization.DataCntxt
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        
        public DbSet<User> Users { get; set; }
        public DbSet<User_Group> User_Groups { get; set; }
        public DbSet<User_State> User_States { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost; Port=5433; Database=APIDb; Username=postgres; Password=Skyrock987645");
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
