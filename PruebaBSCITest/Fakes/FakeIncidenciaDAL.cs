using PruebaBSCI.DAL;
using PruebaBSCI.EN;

namespace PruebaBSCITest.Fakes
{
    public class FakeIncidenciaDAL : IncidenciaDAL
    {
        public FakeIncidenciaDAL() : base("FAKE_CONNECTION") { }

        public override Task<long> CrearIncidenciaAsync(Incidencia incidencia)
        {
            return Task.FromResult(99L);
        }

        public override Task<Incidencia?> ObtenerIncidenciaAsync(long id)
        {
            return Task.FromResult<Incidencia?>(new Incidencia { IdIncidencia = id });
        }

        public override Task<IEnumerable<Incidencia>> ListarIncidenciasAsync(string? estado, int? idCategoria, string? severidad)
        {
            var lista = new List<Incidencia>
            {
                new Incidencia { IdIncidencia = 1 }
            };

            return Task.FromResult<IEnumerable<Incidencia>>(lista);
        }

        public override Task ActualizarEstadoAsync(long id, string accion, string estado, string comentario, string usuario)
        {
            return Task.CompletedTask;
        }
    }
}