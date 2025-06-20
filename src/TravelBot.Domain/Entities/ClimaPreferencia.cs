namespace TravelBot.Domain.Entities;

public class ClimaPreferencia
{
    public Guid Id { get; set; }
    public string Clima { get; set; } = default!;
}