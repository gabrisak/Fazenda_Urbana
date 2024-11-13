using System.Data.Entity;
using PIM.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Usuarios> Usuarios { get; set; }

    public ApplicationDbContext() : base("Data Source=GABI\\SQL14;Initial Catalog=BD_FAZENDA;Integrated Security=True;Encrypt=False;") { }
}s
