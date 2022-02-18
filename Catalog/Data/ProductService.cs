using Catalog.Interfaces;
using Catalog.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Data
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMessageRepository _messageRepository;

        public ProductService()
        {

        }
        public ProductService(IProductRepository productRepository, IMessageRepository messageRepository)
        {
            _productRepository = productRepository;
            _messageRepository = messageRepository;
        }
        public async void CreatProduct(ProductViewModel product)
        {
            var result = _productRepository.CreatProduct(product);
            _productRepository.Save();
            await _messageRepository.SendMessage(product.Name, product.Price);
        }

        public void Deleteproduct(Guid productGuid)
        {
            _productRepository.Deleteproduct(productGuid);
            _productRepository.Save();
        }

        public Task<ProductViewModel> GetProduct(Guid productGuid)
        {
            return _productRepository.GetProduct(productGuid);
        }

        public Task<List<ProductViewModel>> GetProducts()
        {
            return _productRepository.GetProducts();
        }

        public void Update(ProductViewModel product, Guid productGuid)
        {
            _productRepository.Update(product, productGuid);
            _productRepository.Save();
        }
    }
}
