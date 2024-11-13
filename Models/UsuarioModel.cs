namespace PIM.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; } // Idealmente, a senha seria armazenada com hash
    }

}
