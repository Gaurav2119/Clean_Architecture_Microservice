using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable

namespace Cart.Domain.Entities
{
    public sealed class CartList
    {
        public Guid Id { get; private set; }
        public Guid ProductId { get; private set; }

        public string ProductName { get; private set; }
        public ushort QuantityOrdered { get; private set; }
        public float UnitPrice { get; private set; }

        public CartList(Guid productId, string productName, ushort quantityOrdered, float unitPrice)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            ProductName = productName;
            QuantityOrdered = quantityOrdered;
            UnitPrice = unitPrice;
        }
    }
}
