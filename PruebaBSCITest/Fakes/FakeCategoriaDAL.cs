using PruebaBSCI.DAL;
namespace PruebaBSCITest.Fakes
{
    internal class FakeCategoriaDAL : CategoriaDAL

    {
        public FakeCategoriaDAL() : base("FAKE_CONNECTION") { }

        public override Task<bool> ExisteCategoriaAsync(long idCategoria)
        {
            return Task.FromResult(true);
        }

    }
}
