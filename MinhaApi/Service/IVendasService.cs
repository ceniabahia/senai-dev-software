using MinhaApi.Models;

namespace MinhaApi.Services;

public interface IVendasService
{
    IEnumerable<VendasResponse> GetAll();
    VendasResponse? GetById(int id);
    VendasResponse Create(VendasRequest request);
}