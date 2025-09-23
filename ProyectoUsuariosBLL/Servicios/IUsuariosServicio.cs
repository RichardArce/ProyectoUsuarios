using ProyectoUsuariosBLL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoUsuariosBLL.Servicios
{
    public interface IUsuariosServicio
    {
        Task<CustomResponse<UsuarioDto>> ObtenerUsuarioPorIdAsync(int id);

        Task<CustomResponse<List<UsuarioDto>>> ObtenerUsuariosAsync();
    }
}
