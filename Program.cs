using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using PIM.Models;
using PIM.Repository;
using PIM.Data;

var builder = WebApplication.CreateBuilder(args);

// Carrega as configurações do arquivo appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Configuração da conexão com o banco de dados
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registre o repositório de usuários
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

// Registre o serviço de acesso a dados, se necessário
builder.Services.AddScoped<DataService>();

// Adiciona os serviços MVC ao contêiner
builder.Services.AddControllersWithViews();

// ** Adicione suporte à sessão **
builder.Services.AddDistributedMemoryCache(); // Para armazenar dados da sessão na memória
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".PIM.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Expiração da sessão
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; // Necessário para que a sessão funcione no GDPR
});

var app = builder.Build();

// Configure o pipeline de requisição HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// ** Adicione suporte à sessão **
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Inicialize o banco de dados (caso precise)
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    // Se o DataService for realmente necessário:
    var dataService = scope.ServiceProvider.GetRequiredService<DataService>();
    dataService.InitializeDatabase();  // Se o DataService for necessário para inicialização adicional
}

app.Run();
