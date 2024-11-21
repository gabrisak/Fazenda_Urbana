using Microsoft.AspNetCore.Mvc;
using PIM.Repository;
using PIM.Models;

namespace PIM.Controllers
{
    public class UserController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UserController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        // Exibe a tela de Login
        [HttpGet]
        public ActionResult UserLogin()
        {
            return View();
        }

        // Processa a autenticação do Login
        [HttpPost]
        public ActionResult UserLogin(string email, string senha)
        {
            var usuario = _usuarioRepositorio.ObterPorEmail(email);

            if (usuario != null && usuario.Senha == senha)
            {
                HttpContext.Session.SetInt32("UserId", usuario.Id);

                TempData["UserId"] = usuario.Id;

                TempData["SuccessMessage"] = "Login realizado com sucesso!";
                TempData.Keep("SuccessMessage");
                return RedirectToAction("Profile", "User");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "E-mail ou senha inválidos.");
            }

            return View();
        }

        // Exibe a tela de Cadastro
        [HttpGet]
        public ActionResult NewUser()
        {
            return View();
        }

        // Processa o cadastro de novos usuários
        [HttpPost]
        public ActionResult NewUser(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Tentando salvar o usuário
                    _usuarioRepositorio.Criar(usuario);
                    TempData["SuccessMessage"] = "Usuário cadastrado com sucesso!";
                    TempData.Keep("SuccessMessage");
                    return RedirectToAction("UserLogin"); // Redireciona para a tela de Login
                }
                catch (Exception ex)
                {
                    // Adicionando o erro ao ModelState para exibir na tela
                    ModelState.AddModelError(string.Empty, $"Erro ao salvar o usuário: {ex.Message}");
                }
            }

            // Se o modelo não for válido ou ocorrer um erro, retorna para a tela de cadastro
            return View(usuario);
        }
        [HttpGet]
        public ActionResult Profile()
        {
            try
            {
                // Verificando se o ID do usuário está presente no TempData ou na sessão
                var usuarioId = HttpContext.Session.GetInt32("UserId");

                if (usuarioId == null)
                {

                    TempData["ErrorMessage"] = "Usuário não autenticado ou sessão expirada.";
                    return RedirectToAction("UserLogin");
                }

                // Agora, utilizando o ID do usuário armazenado
                var usuario = _usuarioRepositorio.ObterPorID(usuarioId.Value);  // Passando o ID correto

                if (usuario == null)
                {
                    TempData["ErrorMessage"] = "Usuário não encontrado.";
                    return RedirectToAction("UserLogin");
                }

                return View(usuario);  // Exibindo a view do perfil
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Ocorreu um erro ao tentar acessar o perfil do usuário.";
                Console.WriteLine($"Erro: {ex.Message}");
                return RedirectToAction("UserLogin");
            }
        }



        // Exibe a tela de recuperação de usuário/esqueci minha senha
        [HttpGet]
        public ActionResult ForgotUser()
        {
            return View();
        }
    }
}
