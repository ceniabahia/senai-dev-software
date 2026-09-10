namespace MinhaApi.Models;

public class Cliente
{
    public int Id { get; set; }

    public string Nome { get; set; }
        = string.Empty;

    public string email { get; set; }
    = string.Empty;

    public string cpf { get; set; }
    = string.Empty;

    public bool Ativo { get; set; }
        = true;
}