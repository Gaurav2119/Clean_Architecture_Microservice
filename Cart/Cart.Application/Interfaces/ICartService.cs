using Cart.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Application.Interfaces
{
    public interface ICartService
    {
        void Create(CartList createCart);
        IEnumerable<CartList> GetAll();
        CartList GetCart(Guid id);
    }
}
