using MinhaApi.Models;
using MinhaApi.Repositories;
using MinhaApi.Services;

public class VendasService : IVendasService
{
    private readonly IVendasRepository _repo;
    private readonly IClienteRepository _clienteRepo;
    private readonly IProdutoRepository _produtoRepo;

    public VendasService(
        IVendasRepository repo,
        IClienteRepository clienteRepo,
        IProdutoRepository produtoRepo)
    {
        _repo = repo;
        _clienteRepo = clienteRepo;
        _produtoRepo = produtoRepo;
    }

    public IEnumerable<Vendas> GetAll()
        => _repo.GetAll();

    public Vendas? GetById(int id)
        => _repo.GetById(id);

    public Vendas Create(Vendas vendas)
    {
        var cliente = _clienteRepo.GetById(vendas.ClienteId);

        if (cliente == null)
            throw new ArgumentException("Cliente não encontrado.");

        if (!cliente.Ativo)
            throw new ArgumentException("Cliente está inativo.");

        var produto = _produtoRepo.GetById(vendas.ProdutoId);

        if (produto == null)
            throw new ArgumentException("Produto não encontrado.");

        if (!produto.Ativo)
            throw new ArgumentException("Produto está inativo.");

        if (produto.Estoque < vendas.Quantidade)
            throw new ArgumentException("Estoque insuficiente.");

        vendas.ValorTotal = produto.Preco * vendas.Quantidade;
        vendas.DataVenda = DateTime.Now;

       produto.Estoque -= vendas.Quantidade;
       _produtoRepo.AtualizarEstoque(produto.Id, produto.Estoque);
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