using TravelBot.Domain.Entities;

namespace TravelBot.Application.Interfaces;

public interface IClimaService
{
    Task<List<ClimaPreferencia>> ObtenerClimasAsync();
}