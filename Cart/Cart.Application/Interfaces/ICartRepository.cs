using Cart.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Application.Interfaces
{
    public interface ICartRepository
    {
        void CreateCart(CartList item);
        IEnumerable<CartList> GetCarts();
        CartList GetCartById(Guid id);
    }
}
