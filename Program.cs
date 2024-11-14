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

// Carrega as configura��es do arquivo appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Configura��o da conex�o com o banco de dados
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registre o reposit�rio de usu�rios
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

// Registre o servi�o de acesso a dados
builder.Services.AddScoped<DataService>();

// Adiciona os servi�os MVC ao cont�iner
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure o pipeline de requisi��o HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Inicialize o banco de dados (caso precise)
using (var scope = app.Services.CreateScope())
{
    var dataService = scope.ServiceProvider.GetRequiredService<DataService>();
    dataService.InitializeDatabase();
}

app.Run();
