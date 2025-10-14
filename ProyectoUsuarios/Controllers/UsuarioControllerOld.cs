using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProyectoUsuarios.Models;
using ProyectoUsuariosBLL.Dtos;
using ProyectoUsuariosBLL.Servicios;

namespace ProyectoUsuarios.Controllers
{
    public class UsuarioControllerOld : Controller
    {
        private readonly ILogger<UsuarioControllerOld> _logger;

        private readonly IUsuariosServicio _usuariosServicio;

        public UsuarioControllerOld(ILogger<UsuarioControllerOld> logger, IUsuariosServicio usuariosServicio)
        {
            _logger = logger;
            _usuariosServicio = usuariosServicio;
        }

        public async Task<IActionResult> Index()
        {
            var respuesta = await _usuariosServicio.ObtenerUsuariosAsync();
            return View(respuesta.Data);
        }

        //GET: Usuario/Create
        public IActionResult Create()
        {
            return View();
        }
        //insertar con un get VULNERABLE
        //POST: Usuario/Create
        [HttpPost] //->seguridad formulario
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UsuarioDto usuarioDto)
        {
            if (ModelState.IsValid)
            {
                var respuesta = await _usuariosServicio.AgregarUsuarioAsync(usuarioDto);
                if (!respuesta.EsError)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, respuesta.Mensaje);
            }
            return View(usuarioDto);
        }

        //GET: Usuario/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var respuesta = await _usuariosServicio.ObtenerUsuarioPorIdAsync(id);
            if (respuesta.EsError)
            {   
                return NotFound();
            }
            return View(respuesta.Data);
        }

        //POST: Usuario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UsuarioDto usuarioDto)
        {
            if (id != usuarioDto.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var respuesta = await _usuariosServicio.ActualizarUsuarioAsync(usuarioDto);
                if (!respuesta.EsError)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, respuesta.Mensaje);
            }
            return View(usuarioDto);
        }

        //GET: Usuario/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var respuesta = await _usuariosServicio.ObtenerUsuarioPorIdAsync(id);
            if (respuesta.EsError)
            {
                return NotFound();
            }
            return View(respuesta.Data);
        }

        //POST: Usuario/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var respuesta = await _usuariosServicio.EliminarUsuarioAsync(id);
            if (!respuesta.EsError)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, respuesta.Mensaje);
            return View("Delete", respuesta.Data);
        }



    }
}
