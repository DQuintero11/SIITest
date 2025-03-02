using SIITest.Core.Domain.Entities;

namespace SIITest.Core.Interfaces
{
    public interface IBusinessService
    {
        public List<ProductsTax> CalculateTaxByProducts(List<Products> products );
    }
}
