using Microsoft.EntityFrameworkCore;
using RetailSync_Models.DbModels;

namespace RetailSync_Server
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=RetailSync.db");
        }
        public DbSet<UserModel> Users { get; set; }
    }
}
