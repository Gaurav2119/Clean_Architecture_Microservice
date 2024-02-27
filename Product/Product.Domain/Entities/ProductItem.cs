using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable disable

namespace Product.Domain.Entities
{
    public sealed class ProductItem
    {
        public Guid Id { get; private set; }

        public string ProductName { get; private set; }

        public ushort QuantityInStock { get; private set; }

        public float UnitPrice { get; private set; }

        public ProductItem(string productName, ushort quantityInStock, float unitPrice)
        {
            Id = Guid.NewGuid();
            SetProductName(productName);
            SetQuantityInStock(quantityInStock);
            SetUnitPrice(unitPrice);
        }

        public void SetProductName(string productName) { ProductName = productName; }
        public void SetQuantityInStock(ushort quantityInStock) { QuantityInStock = quantityInStock; }
        public void SetUnitPrice(float unitPrice) { UnitPrice = unitPrice; }
    }
}