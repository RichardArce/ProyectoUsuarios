using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProyectoUsuarios.Models;
using ProyectoUsuariosBLL.Dtos;
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
            ViewData["Title"] = "Listado de Usuarios";
            return View();
        }

        public async Task<IActionResult> ObtenerUsuarios()
        {
            var respuesta = await _usuariosServicio.ObtenerUsuariosAsync();
            return Json(respuesta);
        }

        [HttpPost] //->seguridad formulario
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearUsuario(UsuarioDto usuarioDto)
        {
            if(!ModelState.IsValid)
            {
                return Json(new CustomResponse<UsuarioDto> { EsError = true, Mensaje = "Error de validación" });
            }

            var response = await _usuariosServicio.AgregarUsuarioAsync(usuarioDto);
            return Json(response);
        }

        public async Task<IActionResult> ObtenerUsuarioPorId(int id)
        {
            var respuesta = await _usuariosServicio.ObtenerUsuarioPorIdAsync(id);
            return Json(respuesta);
        }

        //POST: Usuario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarUsuario(UsuarioDto usuarioDto)
        {
            if (!ModelState.IsValid)
            {
                return Json(new CustomResponse<UsuarioDto> { EsError = true, Mensaje = "Error de validación" });
            }

            var respuesta = await _usuariosServicio.ActualizarUsuarioAsync(usuarioDto);
            return Json(respuesta);
        }

        //POST: Usuario/Delete/5
        [HttpPost]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            var respuesta = await _usuariosServicio.EliminarUsuarioAsync(id);           
            return Json(respuesta);
        }

    }
}
