using Microsoft.AspNetCore.Mvc;
using PIM.Models;
using System.Data.SqlClient;

namespace PIM.Repository
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly string _connectionString;

        public UsuarioRepositorio()
        {
            _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public Usuario ObterPorEmail(string email)
        {
            Usuario usuario = null;

            // Implementação para buscar usuário pelo email no banco de dados
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Usuarios WHERE Email = @Email";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    usuario = new Usuario
                    {
                        Id = (int)reader["Id"],
                        Nome = reader["Nome"].ToString(),
                        Email = reader["Email"].ToString(),
                        Senha = reader["Senha"].ToString()
                    };
                }
            }

            return usuario;
        }

        public void Criar(Usuario usuario)
        {
            // Implementação para criar um novo usuário no banco de dados
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Usuarios (Nome, Email, Senha) VALUES (@Nome, @Email, @Senha)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Nome", usuario.Nome);
                command.Parameters.AddWithValue("@Email", usuario.Email);
                command.Parameters.AddWithValue("@Senha", usuario.Senha);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
