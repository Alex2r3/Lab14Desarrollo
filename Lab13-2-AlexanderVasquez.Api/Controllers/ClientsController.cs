using MediatR;
using Microsoft.AspNetCore.Mvc;
using Lab13_2_AlexanderVasquez.Application.DTOs.Clients;
using Lab13_2_AlexanderVasquez.Application.UseCases.Clients.Commands;
using Lab13_2_AlexanderVasquez.Application.UseCases.Clients.Queries;

namespace Lab13_2_AlexanderVasquez.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ClientDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var clientes = await mediator.Send(new GetAllClientsQuery());
        return Ok(clientes);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ClientDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var cliente = await mediator.Send(new GetClientByIdQuery(id));
        if (cliente == null)
        {
            return NotFound(new { mensaje = "Cliente no encontrado" });
        }
        return Ok(cliente);
    }

    [HttpPost]
    [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] CreateClientCommand command)
    {
        var clientId = await mediator.Send(command);
        return StatusCode(StatusCodes.Status201Created, new { mensaje = "Cliente creado correctamente", clientId });
    }
}
