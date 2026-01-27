using PruebaBSCI.EN;
using PruebaBSCI.DAL;
namespace PruebaBSCI.BL
{
    public class IncidenciaBL
    {
        private readonly IncidenciaDAL _datos;
        private readonly CategoriaDAL _Cdatos;


        public IncidenciaBL(IncidenciaDAL datos, CategoriaDAL cdatos)
        {
            _datos = datos;
            _Cdatos = cdatos;
        }

        public async Task<long> CrearAsync(Incidencia incidencia)
        {
            if (string.IsNullOrWhiteSpace(incidencia.Titulo))
                throw new Exception("El título no puede estar vacío.");

            if (incidencia.Descripcion.Length < 10)
                throw new Exception("La descripción debe tener al menos 10 caracteres.");

            var severidadesValidas = new[] { "Baja", "Media", "Alta", "Crítica" };
            if (!severidadesValidas.Contains(incidencia.Severidad))
                throw new Exception("La severidad no es válida.");

            if (!await _Cdatos.ExisteCategoriaAsync(incidencia.IdCategoria))
                throw new Exception("La categoría no existe en el catálogo.");

            return await _datos.CrearIncidenciaAsync(incidencia);
        }

        public Task<Incidencia?> ObtenerAsync(long id)
        {
            return _datos.ObtenerIncidenciaAsync(id);
        }

        public Task<IEnumerable<Incidencia>> ListarAsync(string? estado, int? idCategoria, string? severidad)
        {
            return _datos.ListarIncidenciasAsync(estado, idCategoria, severidad);
        }

        public Task ActualizarEstadoAsync(long id, string AccionRealizada, string estado, string comentario, string usuario)
        { 
            return _datos.ActualizarEstadoAsync(id,AccionRealizada,estado, comentario, usuario);
        }
    }
}
