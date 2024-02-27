using Product.Application.Interfaces;
using Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private static readonly List<ProductItem> _productItem = new List<ProductItem>();

        public void CreateProduct(ProductItem product)
        {
            _productItem.Add(product);
        }

        public bool DeleteProduct(Guid id)
        {
            var index = _productItem.FindIndex(existingProduct => existingProduct.Id == id);

            if (index < 0)
            {
                return false;
            }

            _productItem.RemoveAt(index);
            return true;
        }

        public ProductItem GetProduct(Guid id)
        {
            return _productItem.SingleOrDefault(product => product.Id == id);
        }

        public IEnumerable<ProductItem> GetProducts()
        {
            return _productItem.ToList();
        }

        public bool UpdateProduct(ProductItem product)
        {
            var index = _productItem.FindIndex(existingProduct => existingProduct.Id == product.Id);

            if (index < 0)
            {
                return false;
            }

            _productItem[index] = product;
            return true;
        }
    }
}
