using Microsoft.AspNetCore.Mvc;
using TravelBot.Application.Interfaces;
using TravelBot.Infrastructure.Services;
using TravelBot.Domain.Entities;

namespace TravelBot.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController(
    IOpenAIService openAIService,
    IChatStorageService chatStorageService) : ControllerBase
{
    [HttpPost("nuevo")]
    public async Task<IActionResult> CrearChat([FromBody] string nombre, [FromServices] IChatStorageService storage)
    {
        var chat = await storage.CrearChatAsync(string.IsNullOrWhiteSpace(nombre) ? "Chat sin nombre" : nombre);
        return Ok(chat);
    }


    [HttpGet]
    public async Task<IActionResult> ObtenerChats()
    {
        var chats = await chatStorageService.ObtenerTodosAsync();
        return Ok(chats);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObtenerChat(Guid id)
    {
        var chat = await chatStorageService.ObtenerChatAsync(id);
        return chat is null ? NotFound() : Ok(chat);
    }

    [HttpPost("{id:guid}/prompt")]
    public async Task<IActionResult> EnviarPrompt(Guid id, [FromBody] string prompt)
    {
        var userMsg = new ChatMessage { Rol = "user", Texto = prompt };
        await chatStorageService.AgregarMensajeAsync(id, userMsg);

        var respuesta = await openAIService.GetChatCompletionAsync(prompt);

        var botMsg = new ChatMessage { Rol = "assistant", Texto = respuesta };
        await chatStorageService.AgregarMensajeAsync(id, botMsg);

        return Ok(botMsg);
    }
}