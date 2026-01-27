using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaBSCI.BL;
using PruebaBSCI.DAL;
using PruebaBSCI.EN;
using PruebaBSCI.Utilidades;
using PruebaBSCI.Utilidades.Entrada;

namespace PruebaBSCI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncidenciasController : ControllerBase
    {
        private readonly IncidenciaBL _incidenciaBL;

        public IncidenciasController(IncidenciaBL negocio)
        {
            _incidenciaBL = negocio;
        }
        /// <summary>
        /// Registrar Incidencia
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> RegistrarIncidencia([FromBody] Incidencia dt)
        {
            var id = await _incidenciaBL.CrearAsync(dt);
            var response = new ApiResponse<object>(
                success: true,
                message: "Incidencia creada correctamente.",
                data: new { id }
            );
            return Ok(response);

        }
        /// <summary>
        /// obtener incidencia especifica
        /// </summary>
        [HttpGet("{id:long}")]
        public async Task<IActionResult> ObtenerIncidencia(long id)
        {
            var incidencia = await _incidenciaBL.ObtenerAsync(id);
            if (incidencia is null)
            {
                return NotFound(new ApiResponse<object>(
                    success: false,
                    message: "Incidencia no encontrada."
                ));
            }

            return Ok(new ApiResponse<Incidencia>(
                success: true,
                message: "Incidencia obtenida correctamente.",
                data: incidencia
            ));

        }
        /// <summary>
        /// Obtener todas las incidencias
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Listar([FromQuery] string? estado, [FromQuery] int? idCategoria, [FromQuery] string? severidad)
        {
            var lista = await _incidenciaBL.ListarAsync(estado, idCategoria, severidad);
            var response = new ApiResponse<IEnumerable<Incidencia>>(
                success: true,
                message: "Incidencias obtenidas correctamente.", data:
                lista
             );
            return Ok(response);
        }
        /// <summary>
        /// Registro de acciones en bitácora.
        /// </summary>
        [HttpPut("{id:long}/estado")]
        public async Task<IActionResult> ActualizarEstado(long id, [FromBody] ActualizarEstado dt)
        {
            await _incidenciaBL.ActualizarEstadoAsync(id, (string)dt.AccionRealizada, (string)dt.Estado, (string)dt.Comentario, (string)dt.Usuario);
            var response = new ApiResponse<object>(
                    success: true,
                    message: "Estado actualizado correctamente.",
                    data: new { id, nuevoEstado = dt.Estado }
                );

            return Ok(response);

        }
    }
}
