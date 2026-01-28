using Microsoft.AspNetCore.Mvc;
using PruebaBSCI.BL;
using PruebaBSCI.Utilidades;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PruebaBSCI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly CategoriaBL _negocio;

        public CategoriasController(CategoriaBL negocio)
        {
            _negocio = negocio;
        }

        /// <summary>
        /// Lista todas las categorías disponibles.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var categorias = await _negocio.ObtenerCategoriasAsync();
            var response = new ApiResponse<object>(
               success: true,
               message: "Categorías obtenidas correctamente.",
               data: categorias
           );
            return Ok(response);
        }
    }
}
