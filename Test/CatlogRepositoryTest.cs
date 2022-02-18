using Catalog.Data;
using Catalog.Entities;
using Catalog.ViewModels;
using System;
using System.Collections.Generic;
using Xunit;

namespace Test
{
    public class CatlogRepositoryTest
    {
        [Fact]
        public void GetProduct_Returns_Correct_List_Of_Products()
        {
            ProductService _rep = new ProductService();
            List<ProductViewModel> products = _rep.GetProducts().Result;
            Assert.Equal(2, products.Count);

        }
    }
}
