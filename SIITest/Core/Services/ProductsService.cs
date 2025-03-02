using SIITest.Core.Domain.Entities;
using SIITest.Core.Interfaces;

public class ProductsService : IProductsService
{
    private readonly IExternalService _externalService;
    private readonly IBusinessService _businessService;
    private readonly ITaxCalculationStrategy _taxCalculationStrategy;



    public ProductsService(IExternalService externalService, IBusinessService businessService, ITaxCalculationStrategy taxCalculationStrategy)
    {
        _externalService = externalService ?? throw new ArgumentNullException(nameof(externalService));
        _businessService = businessService ?? throw new ArgumentNullException(nameof(businessService));
        _taxCalculationStrategy = taxCalculationStrategy ?? throw new ArgumentNullException(nameof(taxCalculationStrategy));
    }

    /// <summary>
    /// Obtiene todos los productos y les calcula el impuesto
    /// </summary>
    /// <returns>Lista de productos con impuestos aplicados</returns>
    public async Task<List<ProductsTax>> GetAllProducts()
    {
        try
        {
            var productos = await _externalService.ObtenerDatosAsync();
            if (productos == null || productos.Count == 0)
                return new List<ProductsTax>();


            return _taxCalculationStrategy.CalculateIVA(productos); 
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Error al obtener todos los productos", ex);
        }
    }

    /// <summary>
    /// Obtiene un producto por ID y calcula su impuesto
    /// </summary>
    /// <param name="id">ID del producto</param>
    /// <returns>Lista con un solo producto e impuesto aplicado</returns>
    public async Task<List<ProductsTax>> GetProductById(int id)
    {
        try
        {
            var producto = await _externalService.ObtenerDatosByIdAsync(id);

            if (producto == null)
                return new List<ProductsTax>();

            var productsList = new List<Products> { producto };
            return _taxCalculationStrategy.CalculateIVA(productsList);
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Error al obtener el producto con ID {id}", ex);
        }
    }
}
