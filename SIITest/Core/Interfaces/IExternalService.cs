using SIITest.Core.Domain.Entities;

namespace SIITest.Core.Interfaces
{
    public interface IExternalService
    {
        Task<List<Products>> ObtenerDatosAsync();
        Task<Products> ObtenerDatosByIdAsync(int id);
    }
}
