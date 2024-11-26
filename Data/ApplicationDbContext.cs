using Microsoft.EntityFrameworkCore;
using PIM.Models;

namespace PIM.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        // DbSet para a entidade Usuario
        public DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurando a entidade Usuario
            modelBuilder.Entity<Usuario>(entity =>
            {
                // Definindo a chave primária
                entity.HasKey(u => u.Id);

                // Configurando propriedades com restrições
                entity.Property(u => u.Nome)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(u => u.Email)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(u => u.Senha)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(u => u.Endereco)
                      .HasMaxLength(200);

                entity.Property(u => u.Complemento)
                      .HasMaxLength(100);

                entity.Property(u => u.Bairro)
                      .HasMaxLength(100);

                entity.Property(u => u.Numero)
                      .HasMaxLength(15);

                entity.Property(u => u.CEP)
                      .HasMaxLength(9);

                entity.Property(u => u.UF)
                      .HasMaxLength(2);
            });
        }
    }
}
