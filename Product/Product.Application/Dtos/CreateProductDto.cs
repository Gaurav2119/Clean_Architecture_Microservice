using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Dtos
{
    public record CreateProductDto(
        [Required] string productName,
        [Range(0, ushort.MaxValue)] ushort quantityInStock,
        float unitPrice);
}
