using AutoMapper;
using ProyectoUsuariosBLL.Dtos;
using ProyectoUsuariosDAL.Entidades;
using ProyectoUsuariosDAL.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoUsuariosBLL.Servicios
{
    public class UsuarioServicio : IUsuariosServicio
    {
        //Inyección de dependencias
        private readonly IUsuariosRepositorio _usuariosRepositorio;
        private readonly IMapper _mapper;

        public UsuarioServicio(IUsuariosRepositorio usuariosRepositorio, IMapper mapper)
        {
            _usuariosRepositorio = usuariosRepositorio;
            _mapper = mapper;
        }

        public async Task<CustomResponse<UsuarioDto>> ActualizarUsuarioAsync(UsuarioDto usuarioDto)
        {
            var respuesta = new CustomResponse<UsuarioDto>();
            var usuario = _mapper.Map<Usuario>(usuarioDto);
            if (!await _usuariosRepositorio.ActualizarUsuarioAsync(usuario))
            {
                respuesta.EsError = true;
                respuesta.Mensaje = "No se pudo actualizar el usuario";
                return respuesta;
            }
            return respuesta;
        }

        public async Task<CustomResponse<UsuarioDto>> EliminarUsuarioAsync(int id)
        {
            var respuesta = new CustomResponse<UsuarioDto>();

            if (!await _usuariosRepositorio.EliminarUsuarioAsync(id))
            {
                respuesta.EsError = true;
                respuesta.Mensaje = "No se pudo eliminar el usuario";
                return respuesta;
            }

            return respuesta;

        }

        public async Task<CustomResponse<UsuarioDto>> AgregarUsuarioAsync(UsuarioDto usuarioDto)
        {
            var respuesta = new CustomResponse<UsuarioDto>();

            //validaciones de negocio
            if (usuarioDto.Edad > 65)
            {
                respuesta.EsError = true;
                respuesta.Mensaje = "No se pueden agregar usuarios mayores de 65 años";
                return respuesta;
            }

            //El repositorio me indica si pudo o no agregar el usuario
            if(!await _usuariosRepositorio.AgregarUsuarioAsync(_mapper.Map<Usuario>(usuarioDto)))
            {
                respuesta.EsError = true;
                respuesta.Mensaje = "No se pudo agregar el usuario";
                return respuesta;
            }


            return respuesta;
        }
        public async Task<CustomResponse<UsuarioDto>> ObtenerUsuarioPorIdAsync(int id)
        {
            var respuesta = new CustomResponse<UsuarioDto>();           

            var usuario = await _usuariosRepositorio.ObtenerUsuarioPorIdAsync(id);
            
            var validaciones = validar(usuario);
            if(validaciones.EsError)
            {
                return validaciones;
            }

            respuesta.Data = _mapper.Map<UsuarioDto>(usuario);
            return respuesta;
            
        }
        public async Task<CustomResponse<List<UsuarioDto>>> ObtenerUsuariosAsync()
        {
            var respuesta = new CustomResponse<List<UsuarioDto>>();
            var usuarios = await _usuariosRepositorio.ObtenerUsuariosAsync();
            var usuariosDto = _mapper.Map<List<UsuarioDto>>(usuarios);
            respuesta.Data = usuariosDto;
            return respuesta;
        }
        private CustomResponse<UsuarioDto> validar(Usuario usuario)
        {
            var respuesta = new CustomResponse<UsuarioDto>();
            if (usuario == null)
            {
                respuesta.EsError = true;
                respuesta.Mensaje = "Usuario no encontrado";
                return respuesta;
                // puedo agregar N validaciones de negocio
            }
            if (usuario.Edad < 18) //Falla de negocio
            {
                respuesta.EsError = true;
                respuesta.Mensaje = "Usuario menor de edad";
                return respuesta;
                // puedo agregar N validaciones de negocio
            }

            return respuesta;
        }
    }
}
