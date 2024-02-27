using Cart.Application.Interfaces;
using Cart.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Application.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public void Create(CartList createCart)
        {
            _cartRepository.CreateCart(createCart);
        }

        public IEnumerable<CartList> GetAll()
        {
            return _cartRepository.GetCarts();
        }

        public CartList GetCart(Guid id)
        {
            return _cartRepository.GetCartById(id);
        }
    }
}
