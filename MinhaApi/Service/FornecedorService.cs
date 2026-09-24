using MinhaApi.Models;
using MinhaApi.Repositories;
using MinhaApi.Services;

public class FornecedorService : IFornecedorService
{
    private readonly IFornecedorRepository _repo;

    public FornecedorService(IFornecedorRepository repo)
        => _repo = repo;

    public IEnumerable<Fornecedor> GetAll()
        => _repo.GetAll();

    public Fornecedor? GetById(int id)
        => _repo.GetById(id);

    public Fornecedor Create(Fornecedor fornecedor)
    {
        if (string.IsNullOrWhiteSpace(fornecedor.Nome))
            throw new ArgumentException("Nome inválido.");

        if (string.IsNullOrWhiteSpace(fornecedor.Email))
            throw new ArgumentException("Email inválido.");

        _repo.Add(fornecedor);

        return fornecedor;
    }

    public Fornecedor? Update(int id, Fornecedor fornecedor)
    {
        if (_repo.GetById(id) == null)
            return null;

        fornecedor.id = id;

        _repo.Update(fornecedor);

        return fornecedor;
    }

    public bool Delete(int id)
    {
        if (_repo.GetById(id) == null)
            return false;

        _repo.Delete(id);

        return true;
    }
}