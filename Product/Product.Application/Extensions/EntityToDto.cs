using Product.Application.Dtos;
using Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Extensions
{
    public static class EntityToDto
    {
        public static ProductDto AsDto(ProductItem item)
        {
            return new ProductDto(item.Id, item.ProductName, item.QuantityInStock, item.UnitPrice);
        }
    }
}
