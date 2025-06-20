namespace TravelBot.Application.Settings;

public class OpenAISettings
{
    public string ApiKey { get; set; } = default!;
    public string Model { get; set; } = default!;
    public string Endpoint { get; set; } = default!;
    public string Role { get; set; } = default!;
}
