using MinhaApi.Models;
using MinhaApi.Repositories;
using MySqlConnector;

public class VendasRepository
    : IVendasRepository
{
    private readonly string _connectionString;

    public VendasRepository(IConfiguration config)
        => _connectionString = config.GetConnectionString("DefaultConnection")!;

    public void Add(Vendas p)
    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        string sql = @"INSERT INTO vendas
                       (cliente_id, produto_id, quantidade, valor_total)
                       VALUES (@ClienteId, @ProdutoId, @Quantidade, @ValorTotal);
                       SELECT LAST_INSERT_ID();";

        using var cmd = new MySqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("@ClienteId", p.ClienteId);
        cmd.Parameters.AddWithValue("@ProdutoId", p.ProdutoId);
        cmd.Parameters.AddWithValue("@Quantidade", p.Quantidade);
        cmd.Parameters.AddWithValue("@ValorTotal", p.ValorTotal);

        var idGerado = cmd.ExecuteScalar();

        p.Id = Convert.ToInt32(idGerado);
    }

    public IEnumerable<Vendas> GetAll()
    {
        var lista = new List<Vendas>();

        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        string sql = @"SELECT id, cliente_id, produto_id, quantidade,
                              valor_total, data_venda
                       FROM vendas";

        using var cmd = new MySqlCommand(sql, conn);
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            lista.Add(new Vendas
            {
                Id = reader.GetInt32("id"),
                ClienteId = reader.GetInt32("cliente_id"),
                ProdutoId = reader.GetInt32("produto_id"),
                Quantidade = reader.GetInt32("quantidade"),
                ValorTotal = reader.GetDecimal("valor_total"),
                DataVenda = reader.GetDateTime("data_venda")
            });
        }

        return lista;
    }

    public Vendas? GetById(int id)
    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        string sql = @"SELECT id, cliente_id, produto_id, quantidade,
                              valor_total, data_venda
                       FROM vendas
                       WHERE id = @Id";

        using var cmd = new MySqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("@Id", id);

        using var reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            return new Vendas
            {
                Id = reader.GetInt32("id"),
                ClienteId = reader.GetInt32("cliente_id"),
                ProdutoId = reader.GetInt32("produto_id"),
                Quantidade = reader.GetInt32("quantidade"),
                ValorTotal = reader.GetDecimal("valor_total"),
                DataVenda = reader.GetDateTime("data_venda")
            };
        }

        return null;
    }
}