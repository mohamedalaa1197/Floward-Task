using AutoMapper;
using Catalog.Entities;
using Catalog.Interfaces;
using Catalog.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDBContext _dBContext;
        private readonly IMapper _mapper;

        public ProductRepository(ApplicationDBContext dBContext, IMapper mapper)
        {
            _dBContext = dBContext;
            _mapper = mapper;
        }
        public Guid CreatProduct(ProductViewModel product)
        {
            var newProduct = _mapper.Map<Product>(product);
            newProduct.Id = Guid.NewGuid();
            _dBContext.Products.Add(newProduct);
            return newProduct.Id;
        }

        public void Deleteproduct(Guid productGuid)
        {
            var product = _dBContext.Products.FirstOrDefault(x => x.Id == productGuid);
            _dBContext.Products.Remove(product);
        }

        public async Task<ProductViewModel> GetProduct(Guid productGuid)
        {
            var product = _dBContext.Products.FirstOrDefault(x => x.Id == productGuid);
            return _mapper.Map<ProductViewModel>(product);
        }

        public async Task<List<ProductViewModel>> GetProducts()
        {
            var products = _dBContext.Products.ToList();
            return _mapper.Map<List<ProductViewModel>>(products);
        }

        public bool Save()
        {
            var saved = _dBContext.SaveChanges();
            return saved > 0;
        }

        public void Update(ProductViewModel productViewModel, Guid productGuid)
        {
            var product = _dBContext.Products.AsNoTracking().FirstOrDefault(x => x.Id == productGuid);
            var modifiedProduct = _mapper.Map<Product>(productViewModel);
            modifiedProduct.Id = product.Id;
            product = modifiedProduct;
            _dBContext.Entry(product).State = EntityState.Modified;
        }
    }
}
