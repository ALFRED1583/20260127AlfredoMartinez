using PruebaBSCI.EN;
using System.Data;
using System.Data.SqlClient;

namespace PruebaBSCI.DAL
{
    public class IncidenciaDAL
    {
        private readonly string _cn;

        public IncidenciaDAL(string connectionString)
        {
            _cn = connectionString;
        }

        public async Task<long> CrearIncidenciaAsync(Incidencia incidencia)
        {
            using var connection = new SqlConnection(_cn);
            using var command = new SqlCommand("SP_CrearIncidencia", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Titulo", incidencia.Titulo);
            command.Parameters.AddWithValue("@Descripcion", incidencia.Descripcion);
            command.Parameters.AddWithValue("@IdCategoria", incidencia.IdCategoria);
            command.Parameters.AddWithValue("@Severidad", incidencia.Severidad);

            if (incidencia.BitacoraInicial is null)
                command.Parameters.AddWithValue("@BitacoraInicial", DBNull.Value);
            else
                command.Parameters.AddWithValue("@BitacoraInicial", incidencia.BitacoraInicial);

            await connection.OpenAsync();

            var result = await command.ExecuteScalarAsync();

            return Convert.ToInt64(result);
        }

        public async Task<Incidencia?> ObtenerIncidenciaAsync(long idIncidencia)
        {
            using var connection = new SqlConnection(_cn);
            using var command = new SqlCommand("SP_ObtenerIncidencia", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@IdIncidencia", idIncidencia);
            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();
            Incidencia? incidencia = null;
            if (await reader.ReadAsync())
            {
                incidencia = new Incidencia
                {
                    IdIncidencia = reader.GetInt64(reader.GetOrdinal("IdIncidencia")),
                    Titulo = reader.GetString(reader.GetOrdinal("Titulo")),
                    Descripcion = reader.GetString(reader.GetOrdinal("Descripcion")),
                    IdCategoria = reader.GetInt64(reader.GetOrdinal("IdCategoria")),
                    Severidad = reader.GetString(reader.GetOrdinal("Severidad")),
                    FechaRegistro = reader.GetDateTime(reader.GetOrdinal("FechaRegistro")),
                    EstadoActual = reader.GetString(reader.GetOrdinal("EstadoActual")),
                    BitacoraInicial = reader.IsDBNull(reader.GetOrdinal("BitacoraInicial"))
                                      ? null
                                      : reader.GetString(reader.GetOrdinal("BitacoraInicial"))
                };
            }
            if (incidencia is null)
            {
                return null;
            }
            if (await reader.NextResultAsync())
            {
                while (await reader.ReadAsync())
                {
                    incidencia.Bitacoras.Add(new Bitacora
                    {
                        IdBitacora = reader.GetInt64(reader.GetOrdinal("IdBitacora")),
                        IdIncidencia = reader.GetInt64(reader.GetOrdinal("IdIncidencia")),
                        FechaHora = reader.GetDateTime(reader.GetOrdinal("FechaHora")),
                        AccionRealizada = reader.GetString(reader.GetOrdinal("AccionRealizada")),
                        Estado = reader.GetString(reader.GetOrdinal("Estado")),
                        Comentario = reader.GetString(reader.GetOrdinal("Comentario")),
                        Usuario = reader.GetString(reader.GetOrdinal("Usuario"))
                    });
                }
            }
            return incidencia;
        }
        public async Task<IEnumerable<Incidencia>> ListarIncidenciasAsync(string? estado, int? idCategoria, string? severidad)
        {
            var lista = new List<Incidencia>();

            using var connection = new SqlConnection(_cn);
            using var command = new SqlCommand("SP_ListarIncidencias", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Estado", estado ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@IdCategoria", idCategoria ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Severidad", severidad ?? (object)DBNull.Value);

            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(new Incidencia
                {
                    IdIncidencia = reader.GetInt64(reader.GetOrdinal("IdIncidencia")),
                    Titulo = reader.GetString(reader.GetOrdinal("Titulo")),
                    Descripcion = reader.GetString(reader.GetOrdinal("Descripcion")),
                    IdCategoria = reader.GetInt64(reader.GetOrdinal("IdCategoria")),
                    Severidad = reader.GetString(reader.GetOrdinal("Severidad")),
                    FechaRegistro = reader.GetDateTime(reader.GetOrdinal("FechaRegistro")),
                    EstadoActual = reader.GetString(reader.GetOrdinal("EstadoActual")),
                    BitacoraInicial = reader.IsDBNull(reader.GetOrdinal("BitacoraInicial"))
                                      ? null
                                      : reader.GetString(reader.GetOrdinal("BitacoraInicial"))
                });
            }

            return lista;
        }
        public async Task ActualizarEstadoAsync(long idIncidencia,string AccionRealizada, string estado, string comentario, string usuario)
        {
            using var connection = new SqlConnection(_cn);
            using var command = new SqlCommand("SP_ActualizarEstadoIncidencia", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@IdIncidencia", idIncidencia);
            command.Parameters.AddWithValue("@AccionRealizada", AccionRealizada);

            command.Parameters.AddWithValue("@Estado", estado);
            command.Parameters.AddWithValue("@Comentario", comentario);
            command.Parameters.AddWithValue("@Usuario", usuario);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }
    }
}
