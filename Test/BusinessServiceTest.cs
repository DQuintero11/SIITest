using Moq;
using NUnit.Framework;
using SIITest.Core.Bussiness;
using SIITest.Core.Domain.Entities;
using SIITest.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace SIITest.Tests
{
    [TestFixture]
    public class BusinessServiceTests
    {
        private Mock<ITaxCalculationStrategy> _mockTaxCalculationStrategy;
        private BusinessService _businessService;

        [SetUp]
        public void SetUp()
        {

            _mockTaxCalculationStrategy = new Mock<ITaxCalculationStrategy>();
            _businessService = new BusinessService(_mockTaxCalculationStrategy.Object);
        }


        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenTaxCalculationStrategyIsNull()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new BusinessService(null));
        }


        [Test]
        public void CalculateTaxByProducts_ShouldCallCalculateIVA_WhenProductsArePassed()
        {
            // Arrange
            var category = new CategoryBuilder()
                            .SetId(1)
                            .SetName("Electronics")
                            .SetSlug("electronics")
                            .SetImage("image.jpg")
                            .Build();

            var product1 = Products.Create(1, "Product 1", "product-1", 100, "Description 1", category, new List<string> { "image1.jpg" });
            var product2 = Products.Create(2, "Product 2", "product-2", 200, "Description 2", category, new List<string> { "image2.jpg" });
            var products = new List<Products> { product1, product2 };

            var expectedResult = new List<ProductsTax>
            {
                new ProductsTaxBuilder()
                    .SetProduct(product1)
                    .SetTaxCalculationStrategy(_mockTaxCalculationStrategy.Object)
                    .SetTax(21)
                    .Build(),
                new ProductsTaxBuilder()
                    .SetProduct(product2)
                    .SetTaxCalculationStrategy(_mockTaxCalculationStrategy.Object)
                    .SetTax(42)
                    .Build()
            };


            _mockTaxCalculationStrategy.Setup(x => x.CalculateIVA(It.IsAny<List<Products>>()))
                                       .Returns(expectedResult);

            // Act
            var result = _businessService.CalculateTaxByProducts(products);

            // Assert
            _mockTaxCalculationStrategy.Verify(x => x.CalculateIVA(It.IsAny<List<Products>>()), Times.Once);
            Assert.AreEqual(expectedResult, result);
        }
    
        [Test]
        public void CalculateTaxByProducts_ShouldReturnCorrectTaxForProducts()
        {
            // Arrange
            var category = new CategoryBuilder()
                            .SetId(1)
                            .SetName("Electronics")
                            .SetSlug("electronics")
                            .SetImage("image.jpg")
                            .Build();

            var product1 = Products.Create(1, "Product 1", "product-1", 100, "Description 1", category, new List<string> { "image1.jpg" });
            var product2 = Products.Create(2, "Product 2", "product-2", 200, "Description 2", category, new List<string> { "image2.jpg" });
            var products = new List<Products> { product1, product2 };

            var expectedResult = new List<ProductsTax>
            {
                new ProductsTaxBuilder()
                    .SetProduct(product1)
                    .SetTaxCalculationStrategy(_mockTaxCalculationStrategy.Object)
                    .SetTax(21)
                    .Build(),
                new ProductsTaxBuilder()
                    .SetProduct(product2)
                    .SetTaxCalculationStrategy(_mockTaxCalculationStrategy.Object)
                    .SetTax(42)
                    .Build()
            };

            _mockTaxCalculationStrategy.Setup(x => x.CalculateIVA(It.IsAny<List<Products>>()))
                                       .Returns(expectedResult);

            // Act
            var result = _businessService.CalculateTaxByProducts(products);

            // Assert
            Assert.AreEqual(expectedResult.Count, result.Count);
            Assert.AreEqual(expectedResult[0].Id, result[0].Id);
            Assert.AreEqual(expectedResult[1].Tax, result[1].Tax);
        }

        [Test]
        public void CreateProduct_ShouldThrowArgumentNullException_WhenCategoryIsNull()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentNullException>(() => Products.Create(1, "Product", "slug", 100, "Description", null, new List<string>()));
        }
    }
}
