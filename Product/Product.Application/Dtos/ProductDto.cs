using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Dtos
{
    public record ProductDto(Guid Id, string productName, ushort quantityInStock, float unitPrice);
}
