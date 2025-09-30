using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProyectoUsuarios.Models;
using ProyectoUsuariosBLL.Servicios;

namespace ProyectoUsuarios.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly ILogger<UsuarioController> _logger;

        private readonly IUsuariosServicio _usuariosServicio;

        public UsuarioController(ILogger<UsuarioController> logger, IUsuariosServicio usuariosServicio)
        {
            _logger = logger;
            _usuariosServicio = usuariosServicio;
        }

        public async Task<IActionResult> Index()
        {
            var respuesta = await _usuariosServicio.ObtenerUsuariosAsync();
            return View(respuesta.Data);
        }

    }
}
