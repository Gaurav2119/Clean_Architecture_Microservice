using Cart.Application.Dtos;
using Cart.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Application.Extensions
{
    public static class EntityToDto
    {
        public static CartDto AsDto(CartList item)
        {
            return new CartDto(item.Id, item.ProductId, item.ProductName, item.QuantityOrdered, item.UnitPrice);
        }
    }
}
