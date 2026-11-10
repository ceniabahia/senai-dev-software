
using MinhaApi.Models;
using MinhaApi.Repositories;
using MinhaApi.Services;
public class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository _repo;
    private readonly IFotoService _fotoService; // ✨ novo

    public ProdutoService(
        IProdutoRepository repo,
        IFotoService fotoService) // DI injeta automaticamente
    {
        _repo        = repo;
        _fotoService = fotoService;
    }

    public Produto? GetById(int id)
        => _repo.GetById(id);

    public Produto Create(Produto produto)
    {
        if (produto.Preco < 0)
            throw new ArgumentException("Preço inválido");

        _repo.Add(produto);
        return produto;
    }

    public Produto? Update(int id, Produto p)
    {
        if (_repo.GetById(id) == null)
            return null;

        p.Id = id;
        _repo.Update(p);
        return p;
    }

  
public bool Delete(int id)
{
    var produto = _repo.GetById(id);

    if (produto == null)
        return false;

    _repo.Delete(id);

    return true;
}
 // No método ObterTodos() existente, ADICIONAR ao final:
    public List<ProdutoDto> ObterTodos()
    {
        var produtos = _repo.ObterTodos();

        // ✨ Para cada produto, calcular o base64 da foto
        foreach (var p in produtos)
            p.FotoBase64 = _fotoService.LerBase64(p.Foto);

        return produtos;
    }

    // No método ObterPorId() existente, ADICIONAR antes do return:
    public ProdutoDto? ObterPorId(int id)
    {
        var produto = _repo.ObterPorId(id);
        if (produto is null) return null;

        // ✨ Enriquecer com base64 antes de retornar
        produto.FotoBase64 = _fotoService.LerBase64(produto.Foto);
        return produto;
    }
}
