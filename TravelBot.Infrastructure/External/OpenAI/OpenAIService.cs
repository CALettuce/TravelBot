using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using TravelBot.Application.Interfaces;
using TravelBot.Application.Settings;
using TravelBot.Domain.Entities;

namespace TravelBot.Infrastructure.External.OpenAI;

public class OpenAIService(HttpClient httpClient, IOptions<OpenAISettings> options) : IOpenAIService
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly OpenAISettings _settings = options.Value;

    public async Task<string> GetChatCompletionAsync(IEnumerable<ChatMessage> mensajes)
    {
        var mensajesFormateados = new List<object>
        {
            new { role = "system", content = _settings.Role }
        };

        mensajesFormateados.AddRange(mensajes.Select(m => new
        {
            role = m.Rol,
            content = m.Texto
        }));

        var requestBody = new
        {
            model = _settings.Model,
            max_tokens = 500,
            temperature = 0.7,
            messages = mensajesFormateados
        };

        var requestJson = JsonConvert.SerializeObject(requestBody);
        var request = new HttpRequestMessage(HttpMethod.Post, _settings.Endpoint);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _settings.ApiKey);
        request.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync();
        dynamic json = JsonConvert.DeserializeObject(responseBody)!;
        return json.choices[0].message.content;
    }
}