using MinhaApi.Models;
using MinhaApi.Repositories;
using MinhaApi.Services;

public class DepartamentoService : IDepartamentoService
{
    private readonly IDepartamentoRepository _repo;

    public DepartamentoService(IDepartamentoRepository repo)
        => _repo = repo;

    public IEnumerable<Departamento> GetAll()
        => _repo.GetAll();

    public Departamento? GetById(int Id)
        => _repo.GetById(Id);

    public Departamento Create(Departamento departamento)
    {
        if (string.IsNullOrWhiteSpace(departamento.Nome))
            throw new ArgumentException("Nome inválido.");

        if (string.IsNullOrWhiteSpace(departamento.Email))
            throw new ArgumentException("Email inválido.");

        _repo.Add(departamento);

        return departamento;
    }

    public Departamento? Update(int Id, Departamento departamento)
    {
        if (_repo.GetById(Id) == null)
            return null;

        departamento.id = Id;

        _repo.Update(departamento);

        return departamento;
    }

    public bool Delete(int Id)
    {
        if (_repo.GetById(Id) == null)
            return false;

        _repo.Delete(Id);

        return true;
    }

}