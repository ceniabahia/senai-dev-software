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

    public IEnumerable<VendasResponse> GetAll()
    {
        var vendas = _repo.GetAll();

        var lista = new List<VendasResponse>();

        foreach (var venda in vendas)
        {
            var cliente = _clienteRepo.GetById(venda.ClienteId);
            var produto = _produtoRepo.GetById(venda.ProdutoId);

            lista.Add(new VendasResponse
            {
                Id = venda.Id,
                ClienteId = venda.ClienteId,
                ClienteNome = cliente?.Nome ?? "",
                ProdutoId = venda.ProdutoId,
                ProdutoNome = produto?.Nome ?? "",
                Quantidade = venda.Quantidade,
                ValorTotal = venda.ValorTotal,
                DataVenda = venda.DataVenda
            });
        }

        return lista;
    }

    public VendasResponse? GetById(int id)
    {
        var venda = _repo.GetById(id);

        if (venda == null)
            return null;

        var cliente = _clienteRepo.GetById(venda.ClienteId);
        var produto = _produtoRepo.GetById(venda.ProdutoId);

        return new VendasResponse
        {
            Id = venda.Id,
            ClienteId = venda.ClienteId,
            ClienteNome = cliente?.Nome ?? "",
            ProdutoId = venda.ProdutoId,
            ProdutoNome = produto?.Nome ?? "",
            Quantidade = venda.Quantidade,
            ValorTotal = venda.ValorTotal,
            DataVenda = venda.DataVenda
        };
    }

    public VendasResponse Create(VendasRequest request)
    {
        var cliente = _clienteRepo.GetById(request.ClienteId);

        if (cliente == null)
            throw new ArgumentException("Cliente não encontrado.");

        if (!cliente.Ativo)
            throw new ArgumentException("Cliente está inativo.");

        var produto = _produtoRepo.GetById(request.ProdutoId);

        if (produto == null)
            throw new ArgumentException("Produto não encontrado.");

        if (!produto.Ativo)
            throw new ArgumentException("Produto está inativo.");

        if (request.Quantidade <= 0)
            throw new ArgumentException("Quantidade inválida.");

        if (produto.Estoque < request.Quantidade)
            throw new ArgumentException("Estoque insuficiente.");

        var venda = new Vendas
        {
            ClienteId = request.ClienteId,
            ProdutoId = request.ProdutoId,
            Quantidade = request.Quantidade,
            ValorTotal = produto.Preco * request.Quantidade,
            DataVenda = DateTime.Now
        };

        produto.Estoque -= request.Quantidade;

        _produtoRepo.AtualizarEstoque(
            produto.Id,
            produto.Estoque);

        _repo.Add(venda);

        return new VendasResponse
        {
            Id = venda.Id,
            ClienteId = venda.ClienteId,
            ClienteNome = cliente.Nome,
            ProdutoId = venda.ProdutoId,
            ProdutoNome = produto.Nome,
            Quantidade = venda.Quantidade,
            ValorTotal = venda.ValorTotal,
            DataVenda = venda.DataVenda
        };
    }}