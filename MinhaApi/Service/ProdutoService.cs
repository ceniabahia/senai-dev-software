
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
  public async Task<string> SalvarFoto(int id, IFormFile arquivo)
    {
        var produto = _repo.ObterPorId(id)
            ?? throw new KeyNotFoundException(
                   "Produto não encontrado.");

        // Se já tem foto, apaga a antiga do disco
        if (!string.IsNullOrEmpty(produto.Foto))
            _fotoService.Deletar(produto.Foto);

        var nome = await _fotoService.SalvarAsync(arquivo);
        _repo.AtualizarFoto(id, nome);

        // Retorna a data URL — controller manda direto ao React
        return _fotoService.LerBase64(nome)!;
    }

    public void RemoverFoto(int id)
    {
        var produto = _repo.ObterPorId(id)
            ?? throw new KeyNotFoundException(
                   "Produto não encontrado.");

        if (string.IsNullOrEmpty(produto.Foto)) return;

        _fotoService.Deletar(produto.Foto);
        _repo.AtualizarFoto(id, null);
        // O construtor já injeta IProdutoService — não muda
// Apenas adicionar os dois métodos abaixo na classe:

[HttpPost("{id}/foto")]
public async Task<IActionResult> UploadFoto(
    int id, IFormFile arquivo)
{
    if (arquivo == null || arquivo.Length == 0)
        return BadRequest(new { message = "Nenhum arquivo enviado." });

    try
    {
        var fotoBase64 = await _service.SalvarFoto(id, arquivo);
        // Retorna a data URL pronta para o React usar no <img src>
        return Ok(new { fotoBase64 });
    }
    catch (KeyNotFoundException ex)
        { return NotFound(new { message = ex.Message }); }
    catch (ArgumentException ex)
        { return BadRequest(new { message = ex.Message }); }
}

[HttpDelete("{id}/foto")]
public IActionResult RemoverFoto(int id)
{
    try
    {
        _service.RemoverFoto(id);
        return NoContent(); // 204 — sem corpo
    }
    catch (KeyNotFoundException ex)
        { return NotFound(new { message = ex.Message }); }
}
    }

}
