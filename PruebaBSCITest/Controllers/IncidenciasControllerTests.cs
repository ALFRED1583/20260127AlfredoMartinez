using Microsoft.AspNetCore.Mvc;
using PruebaBSCI.BL;
using PruebaBSCI.Controllers;
using PruebaBSCI.EN;
using PruebaBSCI.Utilidades.Entrada;
using PruebaBSCI.Utilidades;
using PruebaBSCITest.Fakes;

namespace PruebaBSCITest.Controllers
{
    public class IncidenciasControllerTests
    {
        private readonly IncidenciasController _controller;

        public IncidenciasControllerTests()
        {
            var fakeIncidenciaDAL = new FakeIncidenciaDAL();
            var fakeCategoriaDAL = new FakeCategoriaDAL();

            var bl = new IncidenciaBL(fakeIncidenciaDAL, fakeCategoriaDAL);

            _controller = new IncidenciasController(bl);
        }

        [Fact]
        public async Task RegistrarIncidencia_RetornaOkConId()
        {
            var incidencia = new Incidencia
            {
                Titulo = "Prueba",
                Descripcion = "Descripción válida",
                Severidad = "Alta",
                IdCategoria = 1
            };

            var result = await _controller.RegistrarIncidencia(incidencia);

            var ok = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<ApiResponse<object>>(ok.Value);

            Assert.True(response.Success);
            Assert.Equal("Incidencia creada correctamente.", response.Message);
        }

        [Fact]
        public async Task ObtenerIncidencia_Existente_RetornaOk()
        {
            var result = await _controller.ObtenerIncidencia(1);

            var ok = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<ApiResponse<Incidencia>>(ok.Value);

            Assert.True(response.Success);
            Assert.Equal(1, response.Data.IdIncidencia);
        }

        [Fact]
        public async Task Listar_RetornaOkConLista()
        {
            var result = await _controller.Listar(null, null, null);

            var ok = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<ApiResponse<IEnumerable<Incidencia>>>(ok.Value);

            Assert.True(response.Success);
            Assert.Single(response.Data);
        }

        [Fact]
        public async Task ActualizarEstado_RetornaOk()
        {
            var entrada = new ActualizarEstado
            {
                AccionRealizada = "Cerrar",
                Estado = "Cerrado",
                Comentario = "Resuelto",
                Usuario = "ALFRED"
            };

            var result = await _controller.ActualizarEstado(1, entrada);

            var ok = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<ApiResponse<object>>(ok.Value);

            Assert.True(response.Success);
            Assert.Equal("Estado actualizado correctamente.", response.Message);
        }
    }

}