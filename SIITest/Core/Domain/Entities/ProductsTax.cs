using Newtonsoft.Json;
using SIITest.Core.Interfaces;

namespace SIITest.Core.Domain.Entities
{
    public class ProductsTax
    {

        public int Id { get; }
        public string Title { get; }
        public string Slug { get; }
        public decimal Price { get; }
        public string Description { get; }
        public Category Category { get; }
        public IReadOnlyList<string> Images { get; }
        public DateTime CreationAt { get; }
        public DateTime UpdatedAt { get; }
        public decimal Tax { get; }

        [JsonConstructor]
        private ProductsTax(Products product, ITaxCalculationStrategy taxCalculationStrategy, decimal tax)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            if (taxCalculationStrategy == null) throw new ArgumentNullException(nameof(taxCalculationStrategy));

            Id = product.Id;
            Title = product.Title;
            Slug = product.Slug;
            Price = product.Price;
            Description = product.Description;
            Category = product.Category;
            Images = product.Images;
            CreationAt = product.CreationAt;
            UpdatedAt = product.UpdatedAt;
            Tax = tax;   
        }

        public static ProductsTax Create(Products product, ITaxCalculationStrategy taxCalculationStrategy, decimal tax)
        {
            return new ProductsTax(product, taxCalculationStrategy, tax);
        }
    }

    public class ProductsTaxBuilder
    {
        private Products _product;
        private ITaxCalculationStrategy _taxCalculationStrategy;
        private decimal _tax;

        public ProductsTaxBuilder SetProduct(Products product) { _product = product; return this; }
        public ProductsTaxBuilder SetTax(decimal tax) { _tax = tax; return this; }
        public ProductsTaxBuilder SetTaxCalculationStrategy(ITaxCalculationStrategy taxCalculationStrategy) { _taxCalculationStrategy = taxCalculationStrategy; return this; }


        public ProductsTax Build()
        {
            if (_taxCalculationStrategy == null)
                throw new ArgumentNullException(nameof(_taxCalculationStrategy));

            return ProductsTax.Create(_product, _taxCalculationStrategy, _tax);
        }
    }
}
