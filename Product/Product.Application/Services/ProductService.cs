using Product.Application.Interfaces;
using Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void CreateProduct(ProductItem product)
        {
            _productRepository.CreateProduct(product);
        }

        public bool DeleteProduct(Guid id)
        {
            return _productRepository.DeleteProduct(id);
        }

        public IEnumerable<ProductItem> GetAllProduct()
        {
            return _productRepository.GetProducts();
        }

        public ProductItem GetProductById(Guid id)
        {

            return _productRepository.GetProduct(id);
        }

        public bool UpdateProduct(ProductItem product)
        {
            return _productRepository.UpdateProduct(product);
        }
    }
}
