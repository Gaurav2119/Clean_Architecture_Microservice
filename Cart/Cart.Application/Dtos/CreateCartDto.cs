using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Application.Dtos
{
    public record CreateCartDto(Guid productId, string productName, ushort quantityOrdered, float unitPrice);
}
