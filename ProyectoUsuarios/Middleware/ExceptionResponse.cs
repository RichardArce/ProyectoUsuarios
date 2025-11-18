using System.Net;

namespace ProyectoUsuarios.Middleware
{
    public record ExceptionResponse(HttpStatusCode StatusCode, string Mensaje);

}
