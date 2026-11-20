using Microsoft.AspNetCore.Mvc;

namespace Lab13_2_AlexanderVasquez.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Get()
    {
        return Ok(new
        {
            estado = "Online",
            mensaje = "Servicio desplegado exitosamente en Render",
            timestamp = DateTime.UtcNow
        });
    }
}
