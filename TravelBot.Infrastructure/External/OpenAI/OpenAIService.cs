using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using TravelBot.Application.Interfaces;
using TravelBot.Application.Settings;

namespace TravelBot.Infrastructure.External.OpenAI;

public class OpenAIService(HttpClient httpClient, IOptions<OpenAISettings> options) : IOpenAIService
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly OpenAISettings _settings = options.Value;

    public async Task<string> GetChatCompletionAsync(string prompt)
    {
        var requestBody = new
        {
            model = _settings.Model,
            max_tokens = 500,
            temperature = 0.7,
            messages = new[]
            {
                new { role = "system", content = _settings.Role },
                new { role = "user", content = prompt }
            }
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