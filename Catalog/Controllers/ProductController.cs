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
        private readonly IProductRepository _productRepository;
        private readonly IMessageRepository _messageRepository;

        public ProductController(IProductRepository productRepository, IMessageRepository messageRepository)
        {
            _productRepository = productRepository;
            _messageRepository = messageRepository;
        }

        [HttpGet("{productGuid}")]
        public async Task<ActionResult<ProductViewModel>> GetProduct(Guid productGuid)
        {
            var product = await _productRepository.GetProduct(productGuid);
            if (product != null)
            {
                return StatusCode(StatusCodes.Status200OK, product);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound, "No Product Found with this Id");
            }
        }
        [HttpGet("All")]
        public async Task<ActionResult<ProductViewModel>> GetAllProducts()
        {
            var products = await _productRepository.GetProducts();
            if (products != null)
            {
                return StatusCode(StatusCodes.Status200OK, products);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound, "No Products Found");
            }
        }
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateProduct([FromBody] ProductViewModel product)
        {
            var result = _productRepository.CreatProduct(product);
            if (_productRepository.Save())
            {
                await _messageRepository.SendMessage(product.Name, product.Price);
                return StatusCode(StatusCodes.Status201Created, result);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed To Create Product");
            }
        }

        [HttpDelete("{productGuid}")]
        public async Task<ActionResult<Guid>> DeleteProduct([FromQuery] Guid productGuid)
        {
            _productRepository.Deleteproduct(productGuid);
            if (_productRepository.Save())
            {
                return StatusCode(StatusCodes.Status200OK, "Product Deleted");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed To Delete Product");
            }
        }

        [HttpPut("{productGuid}")]
        public async Task<ActionResult<Guid>> DeleteProduct([FromBody] ProductViewModel product, Guid productGuid)
        {
            _productRepository.Update(product, productGuid);

            if (_productRepository.Save())
            {
                return StatusCode(StatusCodes.Status200OK, "Product Updated");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed To Update Product");
            }
        }
    }
}
