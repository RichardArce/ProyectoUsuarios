using AutoMapper;
using Moq;
using ProyectoUsuariosBLL.Dtos;
using ProyectoUsuariosBLL.Servicios;
using ProyectoUsuariosDAL.Entidades;
using ProyectoUsuariosDAL.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoUsuariosTEST
{
    public class UsuarioServicioTEST
    {
        //Simular lo que necesito utilizar
        private readonly Mock<IUsuariosRepositorio> _usuariosRepositorioMock;
        private readonly Mock<IMapper> _mapperMock;

        //Simular el elemento a probar
        private readonly UsuarioServicio _usuarioServicio;

        public UsuarioServicioTEST()
        {
            _usuariosRepositorioMock = new ();
            _mapperMock = new ();
            _usuarioServicio = new UsuarioServicio(_usuariosRepositorioMock.Object, _mapperMock.Object);
        }


        [Fact]
        public async Task AgregarUsuarioAsync_UsuarioValido()
        {
            //Arrange
            var usuarioDto = new UsuarioDto { Id = 1, Nombre = "Juan", Apellido = "Pérez", Edad = 30 };
            var usuario = new Usuario { Id = 1, Nombre = "Juan", Apellido = "Pérez", Edad = 30 };
            _mapperMock.Setup(m => m.Map<Usuario>(usuarioDto)).Returns(usuario);
            _usuariosRepositorioMock.Setup(r => r.AgregarUsuarioAsync(usuario)).ReturnsAsync(true);

            //Act
            var resultado = await _usuarioServicio.AgregarUsuarioAsync(usuarioDto);


            //Assert
            Assert.False(resultado.EsError);
            //Assert.NotNull(resultado.Mensaje); //validar correcto
            //Assert.Equal("Acción realizada correctamente", resultado.Mensaje); //validar correcto
        }
        [Fact]
        public async Task AgregarUsuarioAsync_ReglaNegocioUsuarioMayor65()
        {
            //Arrange
            var usuarioDto = new UsuarioDto { Id = 2, Nombre = "Ana", Apellido = "Gómez", Edad = 70 };

            //Act
            var resultado = await _usuarioServicio.AgregarUsuarioAsync(usuarioDto);


            //Assert
            Assert.True(resultado.EsError);
            Assert.Equal("No se pueden agregar usuarios mayores de 65 años", resultado.Mensaje);//otra prueba unitaria
        }

        [Fact]
        public async Task AgregarUsuarioAsync_RepositorioFalla_RetornaError()
        {
            //Arrange
            var usuarioDto = new UsuarioDto { Id = 3, Nombre = "Luis", Apellido = "López", Edad = 40 };
            var usuario = new Usuario { Id = 3, Nombre = "Luis", Apellido = "López", Edad = 40 };

            _mapperMock.Setup(m => m.Map<Usuario>(usuarioDto)).Returns(usuario);
            _usuariosRepositorioMock.Setup(r => r.AgregarUsuarioAsync(usuario)).ReturnsAsync(false);

            //Act
            var resultado = await _usuarioServicio.AgregarUsuarioAsync(usuarioDto);


            //Assert
            Assert.True(resultado.EsError);
            Assert.Equal("No se pudo agregar el usuario", resultado.Mensaje);
        }

    }
}
