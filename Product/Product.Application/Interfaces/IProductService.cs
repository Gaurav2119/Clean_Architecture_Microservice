using Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Interfaces
{
    public interface IProductService
    {
        IEnumerable<ProductItem> GetAllProduct();
        ProductItem GetProductById(Guid id);

        void CreateProduct(ProductItem product);
        bool UpdateProduct(ProductItem product);
        bool DeleteProduct(Guid id);
    }
}
