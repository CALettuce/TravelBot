using Microsoft.AspNetCore.Mvc;
using TravelBot.Application.Interfaces;

namespace TravelBot.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClimaController(IClimaService climaService) : ControllerBase
{
    private readonly IClimaService _climaService = climaService;

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _climaService.ObtenerClimasAsync();
        return Ok(result);
    }
}