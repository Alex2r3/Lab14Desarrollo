using MediatR;
using Microsoft.AspNetCore.Mvc;
using Lab13_2_AlexanderVasquez.Application.DTOs.Products;
using Lab13_2_AlexanderVasquez.Application.UseCases.Products.Commands;
using Lab13_2_AlexanderVasquez.Application.UseCases.Products.Queries;

namespace Lab13_2_AlexanderVasquez.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var productos = await mediator.Send(new GetAllProductsQuery());
        return Ok(productos);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var producto = await mediator.Send(new GetProductByIdQuery(id));
        if (producto == null)
        {
            return NotFound(new { mensaje = "Producto no encontrado" });
        }
        return Ok(producto);
    }

    [HttpPost]
    [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
    {
        var productId = await mediator.Send(command);
        return StatusCode(StatusCodes.Status201Created, new { mensaje = "Producto creado correctamente", productId });
    }
}
