using Cart.Application.Dtos;
using Cart.Application.Extensions;
using Cart.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cart.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public IEnumerable<CartDto> GetAll()
        {
            return _cartService.GetAll().Select(item => EntityToDto.AsDto(item));
        }

        [HttpPost("{id}")]
        public IActionResult CreateOrder(Guid id)
        {
            var existingCart = _cartService.GetCart(id);

            if (existingCart == null)
            {
                return Ok("Add product to checkout!");
            }

            return Ok(new { existingCart, message = "Thanks For CheckOut" });
        }
    }
}
