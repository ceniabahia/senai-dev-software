namespace MinhaApi.Models;

public class VendasRequest
{
    public int ClienteId { get; set; }

    public int ProdutoId { get; set; }

    public int Quantidade { get; set; }
}