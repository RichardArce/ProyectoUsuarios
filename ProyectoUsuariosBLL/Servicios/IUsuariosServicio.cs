using ProyectoUsuariosBLL.Dtos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoUsuariosBLL.Servicios
{
    public interface IUsuariosServicio
    {
        Task<CustomResponse<UsuarioDto>> ObtenerUsuarioPorIdAsync(int id);
        Task<CustomResponse<List<UsuarioDto>>> ObtenerUsuariosAsync();
        Task<CustomResponse<UsuarioDto>> AgregarUsuarioAsync(UsuarioDto usuarioDto);
        Task<CustomResponse<UsuarioDto>> ActualizarUsuarioAsync(UsuarioDto usuarioDto);
        Task<CustomResponse<UsuarioDto>> EliminarUsuarioAsync(int id);
    }
}
