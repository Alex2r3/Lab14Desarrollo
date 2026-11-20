using MediatR;
using Microsoft.AspNetCore.Mvc;
using Lab13_2_AlexanderVasquez.Application.DTOs.Orders;
using Lab13_2_AlexanderVasquez.Application.UseCases.Orders.Commands;
using Lab13_2_AlexanderVasquez.Application.UseCases.Orders.Queries;

namespace Lab13_2_AlexanderVasquez.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<OrderDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var pedidos = await mediator.Send(new GetAllOrdersQuery());
        return Ok(pedidos);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var pedido = await mediator.Send(new GetOrderByIdQuery(id));
        if (pedido == null)
        {
            return NotFound(new { mensaje = "Pedido no encontrado" });
        }
        return Ok(pedido);
    }

    [HttpPost]
    [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] CreateOrderCommand command)
    {
        var orderId = await mediator.Send(command);
        return StatusCode(StatusCodes.Status201Created, new { mensaje = "Pedido creado correctamente", orderId });
    }
}
