using Catalog.Interfaces;
using Catalog.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{productGuid}")]
        public async Task<ActionResult<ProductViewModel>> GetProduct(Guid productGuid)
        {
            var result = await _productService.GetProduct(productGuid);
            return Ok(result ?? new ProductViewModel());

        }
        [HttpGet("All")]
        public async Task<ActionResult<ProductViewModel>> GetAllProducts()
        {
            var result = await _productService.GetProducts();
            return Ok(result ?? new List<ProductViewModel>());
        }
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateProduct([FromBody] ProductViewModel product)
        {
            try
            {
                _productService.CreatProduct(product);
                return Ok(StatusCodes.Status201Created);
            }
            catch (Exception)
            {
                return Ok(StatusCodes.Status500InternalServerError);

            }

        }

        [HttpDelete("{productGuid}")]
        public async Task<ActionResult<Guid>> DeleteProduct([FromQuery] Guid productGuid)
        {
            try
            {
                _productService.Deleteproduct(productGuid);
                return StatusCode(StatusCodes.Status200OK, "Product Deleted");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed To Delete Product");
            }
        }

        [HttpPut("{productGuid}")]
        public async Task<ActionResult<Guid>> UpdateProduct([FromBody] ProductViewModel product, Guid productGuid)
        {

            try
            {
                _productService.Update(product, productGuid);

                return StatusCode(StatusCodes.Status200OK, "Product Updated");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed To Update Product");
            }
        }
    }
}
