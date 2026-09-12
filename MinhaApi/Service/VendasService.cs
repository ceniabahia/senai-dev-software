using MinhaApi.Models;
using MinhaApi.Repositories;
using MinhaApi.Services;

public class VendasService : IVendasService
{
    private readonly IVendasRepository _repo;

    public VendasService(IVendasRepository repo)
        => _repo = repo;

    public IEnumerable<Vendas> GetAll()
        => _repo.GetAll();

    public Vendas? GetById(int id)
        => _repo.GetById(id);

    public Vendas Create(Vendas vendas)
    {
        _repo.Add(vendas);
        return vendas;
    }

    public object Update(int id, Vendas venda)
    {
        throw new NotImplementedException();
    }

    public bool Delete(int id)
    {
        throw new NotImplementedException();
    }
}