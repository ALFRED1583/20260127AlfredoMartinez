namespace PruebaBSCI.Utilidades
{
    public class ControladorExepciones
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ControladorExepciones> _logger;

        public ControladorExepciones(RequestDelegate next, ILogger<ControladorExepciones> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado");

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 500;

                var response = new
                {
                    success = false,
                    message = ex.Message
                };

                await context.Response.WriteAsJsonAsync(response);
            }
        }

    }
}
