using Microsoft.AspNetCore.Mvc;
using TravelBot.Application.Interfaces;

namespace TravelBot.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController(IOpenAIService openAIService) : ControllerBase
{
    private readonly IOpenAIService _openAIService = openAIService;

    [HttpPost("prompt")]
    public async Task<IActionResult> Post([FromBody] string prompt)
    {
        var result = await _openAIService.GetChatCompletionAsync(prompt);
        return Ok(result);
    }
}