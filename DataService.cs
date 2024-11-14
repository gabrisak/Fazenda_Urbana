using Microsoft.EntityFrameworkCore;
using PIM.Models;
using System.Linq;

namespace PIM.Data
{
    public class DataService
    {
        private readonly ApplicationDbContext _context;

        public DataService(ApplicationDbContext context)
        {
            _context = context;
        }
        public void InitializeDatabase()
        {
            _context.Database.EnsureCreated();

            SeedData();
        }
        private void SeedData()
        {
            // Exemplo: Verifica se há algum usuário e, se não houver, insere dados
            if (!_context.Usuario.Any())
            {
                _context.Usuario.Add(new Usuario { Nome = "Admin", Email = "admin@exemplo.com" });
                _context.SaveChanges();
            }
        }
    }
}
