using Microsoft.AspNetCore.Mvc;
using PIM.Repository;
using PIM.Models;

namespace PIM.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public AccountController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string senha)
        {
            var usuario = _usuarioRepositorio.ObterPorEmail(email);
            if (usuario != null && usuario.Senha == senha)
            {
                // Autenticar o usuário e redirecionar para a página inicial
            }
            else
            {
                // Exibir mensagem de erro de login
            }
            return View();
        }

        [HttpGet]
        public ActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastro(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _usuarioRepositorio.Criar(usuario);
                // Redirecionar o usuário para a página de login
            }
            return View(usuario);
        }
    }
}
