using ProyectoUsuariosDAL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoUsuariosDAL.Repositorios
{
    public interface IUsuariosRepositorio
    {
        Task<List<Usuario>> ObtenerUsuariosAsync();
        Task<Usuario> ObtenerUsuarioPorIdAsync(int id);
        Task<bool> AgregarUsuarioAsync(Usuario usuario);
        Task<bool> ActualizarUsuarioAsync(Usuario usuario);
        Task<bool> EliminarUsuarioAsync(int id);

    }
}