using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Product.Application.Dtos;
using Product.Application.Extensions;
using Product.Application.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Product.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IProducer _producer;

        public ProductController(IProductService productService, IProducer producer)
        {
            _productService = productService;
            _producer = producer;
        }
        [HttpGet]
        public IEnumerable<ProductDto> GetProducts()
        {
            return _productService.GetAllProduct().Select(item => EntityToDto.AsDto(item));
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var product = _productService.GetProductById(id);
            if(product == null)
            {
                return NotFound();
            }
            return Ok(EntityToDto.AsDto(product));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddProduct(CreateProductDto createProduct)
        {
            var product = DtoToEntity.CreateDtoAs(createProduct);
            _productService.CreateProduct(product);

            return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateProduct(Guid id, UpdateProductDto updateProduct)
        {
            var existingProduct = _productService.GetProductById(id);
            if(existingProduct == null)
            {
                return NotFound();
            }

            existingProduct.SetProductName(updateProduct.productName);
            existingProduct.SetQuantityInStock(updateProduct.quantityInStock);
            existingProduct.SetUnitPrice(updateProduct.unitPrice);
            return Ok(_productService.UpdateProduct(existingProduct));

        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(Guid id)
        {
            return Ok(_productService.DeleteProduct(id));
        }

        [HttpPost("AddToCart/{id}")]
        public async Task<IActionResult> PlaceOrder(Guid id, ushort Quantity)
        {
            var product = _productService.GetProductById(id);

            if (product != null && Quantity > 0 && Quantity <= product.QuantityInStock)
            {
                var message = new
                {
                    product.ProductName,
                    Quantity,
                    product.UnitPrice
                };
                await _producer.ProduceMessage(product.Id, message);

                product.SetQuantityInStock((ushort)(product.QuantityInStock - Quantity));
                _productService.UpdateProduct(product);

                return Ok("Adding to Cart");
            }
            return BadRequest("Product cannot be added to cart");
        }
    }
}
