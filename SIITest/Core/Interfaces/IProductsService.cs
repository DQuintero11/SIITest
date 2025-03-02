using SIITest.Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SIITest.Core.Interfaces
{
    public interface IProductsService
    {
        Task<List<ProductsTax>> GetAllProducts();

        Task<List<ProductsTax>> GetProductById(int id);
    }
}