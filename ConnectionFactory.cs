using System.Data.SqlClient;
using System.Configuration;
using System;



namespace PIM
{
    public class ConnectionFactory
    {
        public SqlConnection Main()
        {
            //String de conexão
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BD_FAZENDA"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                Console.WriteLine("Conexão com o banco de dados estabelecida com sucesso.");

                // Aqui você pode executar comandos SQL, etc.

            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Erro ao conectar ao banco de dados: {ex.Message}");
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            } //Abrir a conexão
            return connection;

        }

    }
}

