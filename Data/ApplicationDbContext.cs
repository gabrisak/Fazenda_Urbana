using Microsoft.EntityFrameworkCore;
using PIM.Models;

namespace PIM.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Usuario> Usuario { get; set; }
    }
}
