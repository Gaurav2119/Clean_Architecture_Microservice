using Product.Application.Dtos;
using Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Extensions
{
    public static class DtoToEntity
    {
        public static ProductItem CreateDtoAs(CreateProductDto product)
        {
            return new ProductItem(product.productName, product.quantityInStock, product.unitPrice);
        }
    }
}
