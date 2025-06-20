using TravelBot.Application.Interfaces;
using TravelBot.Domain.Entities;

namespace TravelBot.Infrastructure.Services;

public class ClimaService : IClimaService
{
    public async Task<List<ClimaPreferencia>> ObtenerClimasAsync()
    {
        return await Task.FromResult(new List<ClimaPreferencia> {
            new() { Id = Guid.NewGuid(), Clima = "Soleado" },
            new() { Id = Guid.NewGuid(), Clima = "Lluvioso" }
        });
    }
}
