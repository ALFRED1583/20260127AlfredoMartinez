using PruebaBSCI.EN;
using PruebaBSCI.DAL;
namespace PruebaBSCI.BL
{
    public class CategoriaBL
    {
        private readonly CategoriaDAL _datos;

        public CategoriaBL(CategoriaDAL datos)
        {
            _datos = datos;
        }

        public Task<IEnumerable<Categoria>> ObtenerCategoriasAsync()
        { 
            return _datos.ObtenerCategoriasAsync();
        }

    }
}
