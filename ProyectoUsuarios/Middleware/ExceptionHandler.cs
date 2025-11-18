namespace ProyectoUsuarios.Middleware
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate requestDelegate;
        private readonly ILogger<ExceptionHandler> logger;


        public ExceptionHandler(RequestDelegate requestDelegate, ILogger<ExceptionHandler> logger)
        {
            this.requestDelegate = requestDelegate;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await requestDelegate(context);
            }
            catch(Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }



        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            logger.LogError(exception, "Ocurrió una excepción no manejada");

            ExceptionResponse response = exception switch
            {
                NotImplementedException _ => new ExceptionResponse(System.Net.HttpStatusCode.NotImplemented, "Funcionalidad no implementada"),
                KeyNotFoundException _ => new ExceptionResponse(System.Net.HttpStatusCode.NotFound, "Recurso no encontrado"),
                HttpRequestException _ => new ExceptionResponse(System.Net.HttpStatusCode.BadGateway, "Error al comunicarse con la API USUARIOS"),
                _ => new ExceptionResponse(System.Net.HttpStatusCode.InternalServerError, "Ocurrió un error interno del servidor"), //DEFAULT
            };


            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)response.StatusCode;
            await context.Response.WriteAsJsonAsync(response);


        }


    }
}
