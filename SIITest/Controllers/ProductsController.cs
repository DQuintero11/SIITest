using Microsoft.AspNetCore.Mvc;
using SIITest.Core.Domain.Entities;
using SIITest.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SIITest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        /// <summary>
        /// Obtiene todos los productos con sus impuestos aplicados.
        /// </summary>
        /// <returns>Lista de productos con impuestos.</returns>
        [HttpGet]
        public async Task<ActionResult<List<ProductsTax>>> GetProductos()
        {
            try
            {
                var productos = await _productsService.GetAllProducts();
                if (productos == null || productos.Count == 0)
                    return NotFound("No se encontraron productos.");
                return Ok(productos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene un producto por su ID con impuestos aplicados.
        /// </summary>
        /// <param name="id">ID del producto.</param>
        /// <returns>Producto con impuestos o mensaje de error.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductsTax>> GetProductoById(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("El ID del producto debe ser mayor a 0.");

                var producto = await _productsService.GetProductById(id);

                if (producto == null)
                    return NotFound($"Producto con ID {id} no encontrado.");

                return Ok(producto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }

        }
    }
}
