using Microsoft.AspNetCore.Hosting.Server;
using PIM.Models;

namespace PIM.Repository
{
    public interface IUsuarioRepositorio
    {
        Usuario ObterPorEmail(string email);
        void Criar(Usuario usuario);
    }
}
