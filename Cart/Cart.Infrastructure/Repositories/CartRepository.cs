using Cart.Application.Interfaces;
using Cart.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private static readonly List<CartList> _cartItem = new List<CartList>();

        public void CreateCart(CartList item)
        {
            _cartItem.Add(item);
        }

        public IEnumerable<CartList> GetCarts()
        {
            return _cartItem;
        }
        public CartList GetCartById(Guid id)
        {
            return _cartItem.SingleOrDefault(item => item.Id == id);
        }
    }
}
