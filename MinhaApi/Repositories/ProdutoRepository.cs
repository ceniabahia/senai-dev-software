using MinhaApi.Models;
using MinhaApi.Repositories;
using MySqlConnector;

public class ProdutoRepository
    : IProdutoRepository
    
{
  private readonly string _connectionString;
public ProdutoRepository(IConfiguration config) 
      => _connectionString = config.GetConnectionString("DefaultConnection")!;
 public IEnumerable<Produto> GetAll() {
      var lista = new List<Produto>();
      using var conn = new MySqlConnection(_connectionString);
      conn.Open();

      string sql = "SELECT id, nome, preco, estoque, ativo FROM produtos";
      using var cmd = new MySqlCommand(sql, conn);
      using var reader = cmd.ExecuteReader();

      while (reader.Read()) {
          lista.Add(new Produto {
              Id = reader.GetInt32("id"),
              Nome = reader.GetString("nome"),
              Preco = reader.GetDecimal("preco"),
              Estoque = reader.GetInt32("estoque"),
              Ativo = reader.GetBoolean("ativo")
          });
      }
      return lista;
  }


 private static List<Produto> _db = new()
  {
    new Produto { Id=1, Nome="Notebook",
                   Preco=2500m, Estoque=10 },
    new Produto { Id=2, Nome="Mouse",
                   Preco=89.90m, Estoque=50 }
  };
    private string? __connectionString;

    public Produto? GetById(int id)
      => _db.FirstOrDefault(p => p.Id == id);



public void Update(Produto p) {
    using var conn = new MySqlConnection(_connectionString);
    conn.Open();
    string sql = @"UPDATE produtos 
                   SET nome = @Nome, preco = @Preco, estoque = @Estoque, ativo = @Ativo 
                   WHERE id = @Id";
    using var cmd = new MySqlCommand(sql, conn);
    cmd.Parameters.AddWithValue("@Id", p.Id);
    cmd.Parameters.AddWithValue("@Nome", p.Nome);
    cmd.Parameters.AddWithValue("@Preco", p.Preco);
    cmd.Parameters.AddWithValue("@Estoque", p.Estoque);
    cmd.Parameters.AddWithValue("@Ativo", p.Ativo);
    cmd.ExecuteNonQuery();
}

public void Delete(int id) {
    using var conn = new MySqlConnection(_connectionString);
    conn.Open();
    string sql = "DELETE FROM produtos WHERE id = @Id";
    using var cmd = new MySqlCommand(sql, conn);
    cmd.Parameters.AddWithValue("@Id", id);
    cmd.ExecuteNonQuery();
}

public void Add(Produto p) {
    using var conn = new MySqlConnection(_connectionString);
    conn.Open();

    string sql = @"INSERT INTO produtos (nome, preco, estoque, ativo) 
                   VALUES (@Nome, @Preco, @Estoque, @Ativo);
                   SELECT LAST_INSERT_ID();";

    using var cmd = new MySqlCommand(sql, conn);
    cmd.Parameters.AddWithValue("@Nome", p.Nome);
    cmd.Parameters.AddWithValue("@Preco", p.Preco);
    cmd.Parameters.AddWithValue("@Estoque", p.Estoque);
    cmd.Parameters.AddWithValue("@Ativo", p.Ativo);

    // Executa a inserção e recupera o ID gerado pelo MySQL
    var idGerado = cmd.ExecuteScalar();
    p.Id = Convert.ToInt32(idGerado);

      

}

public void AtualizarEstoque(int id, int estoque)

    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        string sql = "UPDATE produtos SET estoque = @Estoque WHERE id = @id";

        using var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", id);
        cmd.Parameters.AddWithValue("@Estoque", estoque);

        
        cmd.ExecuteNonQuery();




    }

}