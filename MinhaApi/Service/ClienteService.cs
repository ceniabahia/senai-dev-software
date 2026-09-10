using MinhaApi.Models;
using MinhaApi.Repositories;
using MinhaApi.Services;

public class ClienteService : IClienteService
{
  private readonly IClienteRepository _repo;

  public ClienteService(IClienteRepository repo)
      => _repo = repo;

  public IEnumerable<Cliente> GetAll()
      => _repo.GetAll();

  public Cliente? GetById(int id)
      => _repo.GetById(id);

  public Cliente Create(Cliente cliente)
  {
      if (cliente.email == "")
          throw new ArgumentException("email valido");
      _repo.Add(cliente);
      return cliente;
  }

  public Cliente? Update(int id, Cliente p)
  {
      if (_repo.GetById(id) == null) return null;
      p.Id = id;
      _repo.Update(p);
      return p;
  }

    public bool Delete(int id)
    {
        throw new NotImplementedException();
    }
}