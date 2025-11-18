using ProyectoUsuarios.Middleware;
using ProyectoUsuariosBLL.Mapeos;
using ProyectoUsuariosBLL.Servicios;
using ProyectoUsuariosDAL.Entidades;
using ProyectoUsuariosDAL.Repositorios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IUsuariosRepositorio, UsuariosRepositorio>();
builder.Services.AddSingleton<IUsuariosServicio, UsuarioServicio>(); //Mala practics

builder.Services.AddAutoMapper(cfg => { }, typeof(MapeoClases));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.UseMiddleware<ExceptionHandler>(); // REGISTRA EL MIDDLEWARE

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "Usuarios",
    pattern: "{controller=Usuario}/{action=Index}/{id?}");

app.Run();
