using Microsoft.AspNetCore.Mvc;

namespace PIM.Controllers
{
    public class UserController : Controller
    {
        public IActionResult UserLogin()
        {
            return View();
        }
        public IActionResult ForgotUser()
        {
            return View();
        }
        public IActionResult NewUser()
        {
            return View();
        }
            private readonly ApplicationDbContext _context;

            public UserController(ApplicationDbContext context)
            {
                _context = context;
            }

            // GET: Usuario
            public async Task<IActionResult> Index()
            {
                return View(await _context.Usuarios.ToListAsync());
            }

            // GET: Usuario/Create
            public IActionResult Create()
            {
                return View();
            }

            // POST: Usuario/Create
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create([Bind("Nome,Endereco,Email,Senha")] Usuario usuario)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(usuario);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(usuario);
            }
        }
    }

}
}
