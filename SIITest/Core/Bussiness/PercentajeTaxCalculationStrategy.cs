using SIITest.Core.Domain.Entities;
using SIITest.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace SIITest.Core.Bussiness
{
    public class PercentageTaxCalculationStrategy : ITaxCalculationStrategy
    {

        public List<ProductsTax> CalculateIVA(List<Products> products)
        {
            var result = new List<ProductsTax>();

            foreach (var product in products)
            {

                decimal tax = Math.Round(product.Price * SIITest.Core.Utilities.AppConstants.TaxRateIVA, 2);


                var productsTax = new ProductsTaxBuilder()
                                    .SetProduct(product)
                                    .SetTaxCalculationStrategy(this)  
                                    .SetTax(tax)
                                    .Build();

                result.Add(productsTax);
            }

            return result;
        }
    }
}
