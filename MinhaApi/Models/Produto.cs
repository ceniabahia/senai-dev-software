
namespace MinhaApi.Models;

public class Produto
{
    internal object? email;

    public int Id { get; set; }

    public string Nome { get; set; }
        = string.Empty;

    public decimal Preco { get; set; }

    public int Estoque { get; set; }

    public bool Ativo { get; set; }
        = true;

    public string? Foto { get; set; }
    public string? FotoBase64 { get; internal set; }

}
