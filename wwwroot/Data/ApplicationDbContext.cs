using System.Data.Entity;

public class ApplicationDbContext : DbContext
{
    public DbSet<Usuários> Usuários { get; set; }

    public ApplicationDbContext() : base("Data Source=GABI\\SQL14;Initial Catalog=BD_FAZENDA;Integrated Security=True;Encrypt=False;") { }
}s
