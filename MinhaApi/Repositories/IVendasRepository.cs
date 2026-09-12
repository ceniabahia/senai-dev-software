using MinhaApi.Models;

namespace MinhaApi.Repositories;

public interface IVendasRepository
{
    IEnumerable<Vendas> GetAll();

    Vendas? GetById(int id);

    void Add(Vendas vendas);
}