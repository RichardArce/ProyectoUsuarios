using ProyectoUsuariosDAL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoUsuariosDAL.Repositorios
{
    public class UsuariosRepositorio : IUsuariosRepositorio
    {
        private List<Usuario> usuarios = new List<Usuario>()
        {
            new Usuario { Id = 1, Nombre = "Juan", Apellido = "Pérez", Edad = 30 },
            new Usuario { Id = 2, Nombre = "María", Apellido = "Gómez", Edad = 25 },
            new Usuario { Id = 3, Nombre = "Carlos", Apellido = "López", Edad = 28 }
        };

        public async Task<bool> ActualizarUsuarioAsync(Usuario usuario)
        {
            var usuarioExistente = usuarios.FirstOrDefault(u => u.Id == usuario.Id);
            usuarioExistente.Nombre = usuario.Nombre;
            usuarioExistente.Apellido = usuario.Apellido;
            usuarioExistente.Edad = usuario.Edad;

            return true;
        }

        public async Task<bool> AgregarUsuarioAsync(Usuario usuario)
        {
            usuario.Id = usuarios.Any() ? usuarios.Max(u => u.Id) + 1 : 1;
            usuarios.Add(usuario);
            return true;
        }

        public async Task<bool> EliminarUsuarioAsync(int id)
        {
            var usuario = usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario != null)
            {
                usuarios.Remove(usuario);
                return true;
            }
            return false;
        }

        public async Task<Usuario> ObtenerUsuarioPorIdAsync(int id)
        {
            var usuario = usuarios.FirstOrDefault(u => u.Id == id);
            return usuario;
        }

        public async Task<List<Usuario>> ObtenerUsuariosAsync()
        {
            return usuarios;
        }
    }
}