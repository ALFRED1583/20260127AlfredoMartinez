using System.Data.SqlClient;
using System.Data;
using PruebaBSCI.EN;
namespace PruebaBSCI.DAL
{
    public class CategoriaDAL
    {
        private readonly string _cn;

        public CategoriaDAL(string connectionString)
        {
            _cn = connectionString;
        }

        public async Task<IEnumerable<Categoria>>ObtenerCategoriasAsync()
        {
            var lista = new List<Categoria>();

            using var connection = new SqlConnection(_cn);
            using var command = new SqlCommand("SP_ObtenerCategorias", connection);

            command.CommandType = CommandType.StoredProcedure;

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(new Categoria
                {
                    IdCategoria = reader.GetInt64(reader.GetOrdinal("IdCategoria")),
                    Nombre = reader.GetString(reader.GetOrdinal("Nombre"))
                });
            }

            return lista;
        }

        public virtual async Task<bool> ExisteCategoriaAsync(long idCategoria)
        {
            using var connection = new SqlConnection(_cn);
            using var command = new SqlCommand("SP_ExisteCategoria", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@IdCategoria", idCategoria);

            await connection.OpenAsync();

            var result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result) > 0;
        }

    }
}
