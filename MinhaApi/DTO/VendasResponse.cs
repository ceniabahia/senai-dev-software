namespace MinhaApi.Models;

public class VendasResponse
{
    public int Id { get; set; }

    public int ClienteId { get; set; }

    public string ClienteNome { get; set; } = string.Empty;

    public int ProdutoId { get; set; }

    public string ProdutoNome { get; set; } = string.Empty;

    public int Quantidade { get; set; }

    public decimal ValorTotal { get; set; }

    public DateTime DataVenda { get; set; }



    
}