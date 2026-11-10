using System.Text.Json.Serialization;

public class ProdutoDto
{
    public int     Id      { get; set; }
    public string  Nome    { get; set; } = string.Empty;
    public decimal Preco   { get; set; }
    public int     Estoque { get; set; }
    public bool    Ativo   { get; set; }

    // Nome do arquivo em disco — NÃO enviado ao frontend
    [JsonIgnore]
    public string? Foto    { get; set; }

    // Data URL calculada pelo Service — ENVIADA ao frontend
    public string? FotoBase64 { get; set; } // ✨ NOVO
}