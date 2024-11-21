using PIM.Models;
using PIM.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace PIM.Repository
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly ApplicationDbContext _context;  // Usando o ApplicationDbContext

        public UsuarioRepositorio(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context)); // Inicializando o contexto
        }

        // Método para criar um usuário
        public void Criar(Usuario usuario)
        {
            if (usuario == null)
                throw new ArgumentNullException(nameof(usuario), "O objeto usuário não pode ser nulo.");

            // Validações do usuário
            if (string.IsNullOrWhiteSpace(usuario.Nome))
                throw new ArgumentException("O nome é obrigatório.", nameof(usuario.Nome));

            if (string.IsNullOrWhiteSpace(usuario.Email))
                throw new ArgumentException("O e-mail é obrigatório.", nameof(usuario.Email));

            if (string.IsNullOrWhiteSpace(usuario.Senha))
                throw new ArgumentException("A senha é obrigatória.", nameof(usuario.Senha));

            if (string.IsNullOrWhiteSpace(usuario.Endereco))
                throw new ArgumentException("O endereço é obrigatório.", nameof(usuario.Endereco));

            if (string.IsNullOrWhiteSpace(usuario.Bairro))
                throw new ArgumentException("O bairro é obrigatório.", nameof(usuario.Bairro));

            if (string.IsNullOrWhiteSpace(usuario.UF))
                throw new ArgumentException("A UF é obrigatória.", nameof(usuario.UF));

            try
            {
                // Usando o Entity Framework para salvar o usuário
                _context.Usuario.Add(usuario);
                _context.SaveChanges(); // Persistindo as alterações no banco de dados
            }
            catch (DbUpdateException dbEx)
            {
                // Se ocorrer erro ao salvar no banco
                Console.WriteLine($"Erro ao salvar usuário no banco: {dbEx.Message}");
                throw new InvalidOperationException("Erro ao salvar o usuário no banco de dados.", dbEx);
            }
            catch (Exception ex)
            {
                // Qualquer outro erro inesperado
                Console.WriteLine($"Erro inesperado: {ex.Message}");
                throw new ApplicationException("Erro inesperado ao salvar o usuário.", ex);
            }
        }

        // Método para obter usuário por e-mail
        public Usuario ObterPorEmail(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                    throw new ArgumentException("O e-mail não pode ser nulo ou em branco.", nameof(email));

                // Usando ToLower para garantir uma comparação insensível a maiúsculas/minúsculas
                var usuario = _context.Usuario
                    .FirstOrDefault(u => u.Email.ToLower() == email.ToLower()); // Comparando e-mails de forma case-insensitive

                if (usuario == null)
                {
                    // Exceção lançada caso o usuário não seja encontrado
                    throw new KeyNotFoundException($"Usuário com o e-mail {email} não encontrado.");
                }

                return usuario;
            }
            catch (ArgumentException argEx)
            {
                // Tratar erros de argumento (e-mail inválido)
                Console.WriteLine($"Erro de argumento: {argEx.Message}");
                throw new ArgumentException("Erro na validação do e-mail.", argEx);
            }
            catch (DbUpdateException dbEx)
            {
                // Erro específico de atualização no banco de dados
                Console.WriteLine($"Erro ao acessar o banco de dados: {dbEx.Message}");
                throw new InvalidOperationException("Erro ao acessar o banco de dados.", dbEx);
            }
            catch (Exception ex)
            {
                // Exceção genérica para outros casos
                Console.WriteLine($"Erro inesperado: {ex.Message}");
                throw new ApplicationException("Erro inesperado ao acessar o banco de dados.", ex);
            }
        }

        // Método para obter usuário por ID
        public Usuario ObterPorID(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("O ID não pode ser zero ou negativo.", nameof(id));

                // Busca o usuário pelo ID
                var usuario = _context.Usuario.FirstOrDefault(u => u.Id == id);

                if (usuario == null)
                {
                    throw new KeyNotFoundException($"Usuário com o ID {id} não encontrado.");
                }

                return usuario;
            }
            catch (Exception ex)
            {
                // Captura qualquer erro inesperado e o loga
                Console.WriteLine($"Erro ao acessar o banco de dados: {ex.Message}");
                throw new Exception("Erro ao acessar o banco de dados.", ex);
            }
        }

    }
}
