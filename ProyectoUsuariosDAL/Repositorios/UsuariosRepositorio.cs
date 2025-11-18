using ProyectoUsuariosDAL.Entidades;
using ProyectoUsuariosDAL.RespuestasAPIS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
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

        private readonly HttpClient _httpClient;

        public UsuariosRepositorio(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


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
            var informacion = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(usuario),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync("https://localhost:7267/Usuarios", informacion);


            return response.IsSuccessStatusCode;

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
            //SP //API // ETC
            var usuario = usuarios.FirstOrDefault(u => u.Id == id);
            return usuario;
        }

        public async Task<List<Usuario>> ObtenerUsuariosAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<RespuestaApiUsuarios<List<Usuario>>>("https://localhost:7267/Usuarios");

            return response.Data ?? new List<Usuario>(); //DEPENDEMOS DE UN COMPONENTE EXTERNO
        }
    }
}