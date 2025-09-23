using AutoMapper;
using ProyectoUsuariosBLL.Dtos;
using ProyectoUsuariosDAL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoUsuariosBLL.Mapeos
{
    public class MapeoClases : Profile
    {
        public MapeoClases()
        {
            CreateMap<Usuario, UsuarioDto>();
            CreateMap<UsuarioDto, Usuario>();
        }
    }
}