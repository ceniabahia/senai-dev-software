using MinhaApi.Models;

namespace MinhaApi.Services;

public interface IVendasService
{
    IEnumerable<Vendas> GetAll();

    Vendas? GetById(int id);

    Vendas Create(Vendas vendas);
    object Update(int id, Vendas venda);
    bool Delete(int id);
}