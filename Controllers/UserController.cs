using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; // Para HttpContext.Session
using System.Linq;
using PIM.Models;

public class UserController : Controller
{
    private readonly ApplicationDbContext _context;

    public UserController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult Login(string email, string senha)
    {
        // Realizar a consulta no banco para verificar se as credenciais estão corretas
        var user = _context.Usuários.SingleOrDefault(u => u.Email == email && u.Senha == senha);

        if (user != null)
        {
            // Autenticação bem-sucedida
            // Salvar dados do usuário na sessão
            HttpContext.Session.SetInt32("UserId", user.Id); // Salva o Id do usuário na sessão
            return RedirectToAction("Index", "Home");
        }

        // Retornar uma mensagem de erro de autenticação
        ViewBag.ErrorMessage = "Credenciais inválidas.";
        return View();
    }
}

public class ApplicationDbContext
{
    public object Usuários { get; internal set; }
}