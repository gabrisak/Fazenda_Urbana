using Microsoft.AspNetCore.Hosting.Server;
using PIM.Models;

namespace PIM.Repository
{
    public interface IUsuarioRepositorio
    {
        Usuario ObterPorEmail(string email);
        Usuario ObterPorID(int id);
        void Criar(Usuario usuario);
    }
}
