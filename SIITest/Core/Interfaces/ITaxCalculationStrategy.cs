using SIITest.Core.Domain.Entities;

namespace SIITest.Core.Interfaces;
public interface ITaxCalculationStrategy
{
    List<ProductsTax> CalculateIVA(List<Products> products);
}
