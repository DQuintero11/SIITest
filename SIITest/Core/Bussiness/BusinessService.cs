using SIITest.Core.Domain.Entities;
using SIITest.Core.Interfaces;


namespace SIITest.Core.Bussiness
{
    public class BusinessService : IBusinessService
    {
        private readonly ITaxCalculationStrategy _taxCalculationStrategy;

       
        public BusinessService(ITaxCalculationStrategy taxCalculationStrategy)
        {
            _taxCalculationStrategy = taxCalculationStrategy ?? throw new ArgumentNullException(nameof(taxCalculationStrategy));
        }


        public List<ProductsTax> CalculateTaxByProducts(List<Products> products)
        {

            return _taxCalculationStrategy.CalculateIVA(products);  
        }
    }
}