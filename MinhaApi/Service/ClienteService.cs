
using MinhaApi.Models;
using MinhaApi.Repositories;
using MinhaApi.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _repo;

    public ClienteService(IClienteRepository repo)
        => _repo = repo;

    public IEnumerable<Cliente> GetAll()
    {
        return _repo.GetAll();
    }

    public Cliente? GetById(int id)
    {
        return _repo.GetById(id);
    }

    public Cliente Create(Cliente cliente)
    {
        if (string.IsNullOrWhiteSpace(cliente.Nome))
            throw new ArgumentException("Nome do cliente inválido.");

        if (string.IsNullOrWhiteSpace(cliente.email))
            throw new ArgumentException("E-mail do cliente inválido.");

        if (string.IsNullOrWhiteSpace(cliente.cpf))
            throw new ArgumentException("CPF do cliente inválido.");

        _repo.Add(cliente);

        return cliente;
    }

    public Cliente? Update(int id, Cliente cliente)
    {
        var clienteExistente = _repo.GetById(id);

        if (clienteExistente == null)
            return null;

        if (string.IsNullOrWhiteSpace(cliente.Nome))
            throw new ArgumentException("Nome do cliente inválido.");

        if (string.IsNullOrWhiteSpace(cliente.email))
            throw new ArgumentException("E-mail do cliente inválido.");

        if (string.IsNullOrWhiteSpace(cliente.cpf))
            throw new ArgumentException("CPF do cliente inválido.");

        cliente.Id = id;

        _repo.Update(cliente);

        return cliente;
    }

    public bool Delete(int id)
    {
        var cliente = _repo.GetById(id);

        if (cliente == null)
            return false;

        _repo.Delete(id);

        return true;
    }
}
