using System.Data.Entity;

public class ApplicationDbContext : DbContext
{
    public DbSet<Usuários> Usuários { get; set; }

    public ApplicationDbContext() : base("Server=GABI\\SQL14;Database=BD_FAZENDA;Trusted_Connection=True;") { }
}s
