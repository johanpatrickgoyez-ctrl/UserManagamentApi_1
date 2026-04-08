using Microsoft.EntityFrameworkCore;
using UserManagamentApi_1.Models;

namespace UserManagamentApi_1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}