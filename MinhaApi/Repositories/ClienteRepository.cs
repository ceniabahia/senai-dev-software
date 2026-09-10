using MinhaApi.Models;
using MinhaApi.Repositories;
using MySqlConnector;

public class ClienteRepository
    : IClienteRepository
    
{
  private readonly string _connectionString;
public ClienteRepository(IConfiguration config) 
      => _connectionString = config.GetConnectionString("DefaultConnection")!;
 public IEnumerable<Cliente> GetAll() {
      var lista = new List<Cliente>();
      using var conn = new MySqlConnection(_connectionString);
      conn.Open();

      string sql = "SELECT id, nome, email, cpf, ativo FROM cliente";
      using var cmd = new MySqlCommand(sql, conn);
      using var reader = cmd.ExecuteReader();

      while (reader.Read()) {
          lista.Add(new Cliente {
              Id = reader.GetInt32("id"),
              Nome = reader.GetString("nome"),
              email = reader.GetString("email"),
              cpf = reader.GetString("cpf"),
              Ativo = reader.GetBoolean("ativo")
          });
      }
      return lista;
  }


 


public void Update(Cliente p) {
    using var conn = new MySqlConnection(_connectionString);
    conn.Open();
    string sql = @"UPDATE cliente 
                   SET nome = @Nome, email = @email, cpf = @cpf, ativo = @Ativo 
                   WHERE id = @Id";
    using var cmd = new MySqlCommand(sql, conn);
    cmd.Parameters.AddWithValue("@Id", p.Id);
    cmd.Parameters.AddWithValue("@Nome", p.Nome);
    cmd.Parameters.AddWithValue("@email", p.email);
    cmd.Parameters.AddWithValue("@cpf", p.cpf);
    cmd.Parameters.AddWithValue("@Ativo", p.Ativo);
    cmd.ExecuteNonQuery();
}

public void Delete(int id) {
    using var conn = new MySqlConnection(_connectionString);
    conn.Open();
    string sql = "DELETE FROM cliente WHERE id = @Id";
    using var cmd = new MySqlCommand(sql, conn);
    cmd.Parameters.AddWithValue("@Id", id);
    cmd.ExecuteNonQuery();
}

public void Add(Cliente p) {
    using var conn = new MySqlConnection(_connectionString);
    conn.Open();

    string sql = @"INSERT INTO cliente (nome, email, cpf, ativo) 
                   VALUES (@Nome, @email, @cpf, @Ativo);
                   SELECT LAST_INSERT_ID();";

    using var cmd = new MySqlCommand(sql, conn);
    cmd.Parameters.AddWithValue("@Nome", p.Nome);
    cmd.Parameters.AddWithValue("@email", p.email);
    cmd.Parameters.AddWithValue("@cpf", p.cpf);
    cmd.Parameters.AddWithValue("@Ativo", p.Ativo);

    // Executa a inserção e recupera o ID gerado pelo MySQL
    var idGerado = cmd.ExecuteScalar();
    p.Id = Convert.ToInt32(idGerado);

      
}
Cliente? IClienteRepository.GetById(int id)
{
    using var conn = new MySqlConnection(_connectionString);
    conn.Open();

    string sql = "SELECT id, nome, email, cpf, ativo FROM cliente WHERE id = @Id";

    using var cmd = new MySqlCommand(sql, conn);

    cmd.Parameters.AddWithValue("@Id", id);

    using var reader = cmd.ExecuteReader();

    if (reader.Read())
    {
        return new Cliente
        {
            Id = reader.GetInt32("id"),
            Nome = reader.GetString("nome"),
            email = reader.GetString("email"),
            cpf = reader.GetString("cpf"),
            Ativo = reader.GetBoolean("ativo")
        };
    }

    return null;
}
   
}